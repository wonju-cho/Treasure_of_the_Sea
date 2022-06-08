using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//death, combat 

public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    private CharacterController controller;
    private PlayerController playerController;

    [Tooltip("Need to add the healthbar of the playerUI prefab")]
    public Image HealthBar;

    private bool isPlayerDead = false;
    private bool triggerOnce = false;
    private float fillSpeed = 2f;

    Vector3 playerSpawnPosition;

    [SerializeField]
    float playerHP = 10;
    private float currentHP;

    public GameObject projectile;
    public Transform projectilePoint;


    // Start is called before the first frame update
    void Start()
    {
        currentHP = playerHP;
        playerSpawnPosition = transform.position;

        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();

        if (!controller)
            Debug.Log("There is no controller in the PlayerManager script");

        if (!HealthBar)
            Debug.Log("There is no healthbar in the playermanger script");

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1) && isPlayerDead)
        {
            PlayerRespawn();
        }

        //test health bar ui
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentHP--;
        }

        if (currentHP <= 0)
        {
            isPlayerDead = true;
        }

        if (isPlayerDead)
        {
            PlayerDeath();
        }
        
        HealthBarFill();
    }

    float GetCurrentHP() { return currentHP; }

    public void TakeDamge(int damage) 
    {
        Debug.Log("Player take damage: " + currentHP);
        currentHP -= damage; 
    }

    void HealthBarFill()
    {
        HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, (currentHP / playerHP), fillSpeed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Sea"))
        {
            isPlayerDead = true;
        }
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    void PlayerDeath()
    {
        playerController.enabled = false;
        animator.SetBool("IsIdle", false);

        if(isPlayerDead && triggerOnce == false)
        {
            triggerOnce = true;
            animator.ResetTrigger("Death");
            animator.SetTrigger("Death");
        }
    }

    IEnumerator WaitForSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void PlayerRespawn()
    {
        playerController.enabled = true;

        currentHP = playerHP;
        animator.SetBool("IsIdle", true);

        controller.enabled = false;
        controller.transform.position = playerSpawnPosition;
        controller.enabled = true;

        isPlayerDead = false;
        triggerOnce = false;

    }

    public void Shoot()
    {
        Debug.Log("player shoot");
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up * 7f, ForceMode.Impulse);
    }
}
