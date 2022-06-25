//death, combat 
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    //public GameObject crossHairUI;

    //for testing the arrow
    private InventoryHolder inventoryHolder;
    public InventoryItemData cottonTest;
    public InventoryItemData arrowTest;

    Camera mainCamera;
    Vector3 worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        currentHP = playerHP;
        playerSpawnPosition = transform.position;

        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        inventoryHolder = GetComponent<InventoryHolder>();

        if (!controller)
            Debug.Log("There is no controller in the PlayerManager script");

        if (!HealthBar)
            Debug.Log("There is no healthbar in the playermanger script");

        if (!inventoryHolder)
            Debug.Log("There is no inventory holder in player manager script");

    }

    // Update is called once per frame
    void Update()
    {
        TestingItems();

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

        if (Input.GetButtonDown("Fire1") && controller.isGrounded)
        {
            if (animator.GetBool("IsShooting") == false)
            {
                animator.SetBool("IsShooting", true);
            }

        //    Vector3 mousePos = Input.mousePosition;
        //    mousePos.z = mainCamera.nearClipPlane;
        //    worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        //    crossHairUI.transform.position = worldPosition;
        //    crossHairUI.SetActive(true);
        //}
        //else { crossHairUI.SetActive(false);
        }
    }

    float GetCurrentHP() { return currentHP; }

    public void TakeDamge(int damage) 
    {
        //Debug.Log("Player take damage: " + currentHP);
        currentHP -= damage; 
        if(currentHP > 0)
        {
            animator.ResetTrigger("GetDamage");
            animator.SetTrigger("GetDamage");
        }
    }

    void TestingItems()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            inventoryHolder.InventorySystem.AddToInventory(cottonTest, 1);
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            inventoryHolder.InventorySystem.AddToInventory(arrowTest, 1);
        }
    }

    void HealthBarFill()
    {
        HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, (currentHP / playerHP), fillSpeed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Sea"))
        {
            currentHP = 0;
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

    public void Shoot(/*vector3 hitpoint*/)
    {
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 7f, ForceMode.Impulse);
        Vector3 dir = transform.forward;
        rb.AddForce(dir*20f, ForceMode.VelocityChange);
    }
}
