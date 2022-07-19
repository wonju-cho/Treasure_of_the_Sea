//death, combat 
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [System.Serializable]
    public class AnimationStrings
    {
        public string aim = "Aim";
        public string pull = "PullString";
        public string fire = "Fire";

        public string aim_input = "Fire1";
        public string fire_input = "Fire2";
    }

    [Header("Player SFX")]
    public AudioSource itemSFX;
    public AudioSource eatingSFX;
    public AudioSource shootingSFX;
    public AudioSource aimingSFX;
    public AudioSource craftingSFX;
    public AudioSource dyingSFX;
    public AudioSource hittingSFX;
    public AudioSource walkingSFX;

    [SerializeField]
    public AnimationStrings animStrings;

    bool is_aiming = false;

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

    //for testing the arrow
    private InventoryHolder inventoryHolder;
    public InventoryItemData cottonTest;
    public InventoryItemData arrowTest;

    Camera mainCamera;
    Vector3 worldPosition;

    public bool testAim;

    Crafting crafting;
    private int NumOfSkull = 0;
    private int NumOfSkullInWorld;

    bool aiming_trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        currentHP = playerHP;
        playerSpawnPosition = transform.position;

        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        inventoryHolder = GetComponent<InventoryHolder>();
        crafting = GameObject.FindWithTag("Crafting").GetComponent<Crafting>();

        NumOfSkull = 0;
        NumOfSkullInWorld = GameObject.FindGameObjectsWithTag("Chest").Length;

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
        if (Input.GetKeyDown(KeyCode.F1) && isPlayerDead)
        {
            PlayerRespawn();
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

        //is_aiming = Input.GetButton(animStrings.aim_input);

        if (testAim)
            is_aiming = true;

        CharacterAim(is_aiming);

        if(is_aiming)
        {
            if(aiming_trigger == false)
            {
                aimingSFX.Play();
                aiming_trigger = true;
            }

            playerController.Aim();
            playerController.bowScript.EquipWeapon();
            //CharacterPullString(Input.GetButton(animStrings.fire_input));

            if(Input.GetButtonUp(animStrings.aim_input))
            {
                shootingSFX.Play();
                CharacterFire();
                aiming_trigger = false;
                if(playerController.hitDetected == true)
                {
                    playerController.bowScript.Fire(playerController.hit.point);
                }
                else
                {
                    playerController.bowScript.Fire(playerController.ray.GetPoint(300));
                }
            }
        }
        else
        {
            playerController.bowScript.UnEquipWeapon();
            playerController.bowScript.ReMoveCrossHair();
            playerController.bowScript.DisableArrow();
            playerController.bowScript.ReleaseString();
        }

        
        is_aiming = Input.GetButton(animStrings.aim_input);

        //Debug.Log(playerController.bowScript.bowSettings.arrowCount);
        if (playerController.bowScript.bowSettings.arrowCount <= 0)
        {
            is_aiming = false;
        }

        

        if (Input.GetButtonDown("Fire1") && controller.isGrounded)
        {
            //if (animator.GetBool("IsShooting") == false)
            //{
            //    animator.SetBool("IsShooting", true);
            //}

        //    Vector3 mousePos = Input.mousePosition;
        //    mousePos.z = mainCamera.nearClipPlane;
        //    worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        //    crossHairUI.transform.position = worldPosition;
        //    crossHairUI.SetActive(true);
        //}
        //else { crossHairUI.SetActive(false);
        }


    }

    private void LateUpdate()
    {
        if (testAim)
        {
            playerController.RotateCharacterSpine();
        }
    }

    public void CharacterAim(bool aiming)
    {
        if(!crafting.getCraftingActivated())
        {
            animator.SetBool(animStrings.aim, aiming);
        }
    }

    public void CharacterPullString(bool pulling)
    {
        animator.SetBool(animStrings.pull, pulling);
    }

    public void CharacterFire()
    {
        animator.SetTrigger(animStrings.fire);   
    }

    private void AimShoot()
    {
        if(animator.GetBool("IsShooting") == false)
        {
            animator.SetBool("IsShooting", true);
        }
    }

    float GetCurrentHP() { return currentHP; }

    public void TakeDamge(int damage) 
    {
        //Debug.Log("Player take damage: " + currentHP);
        currentHP -= damage;
        

        if(currentHP > 0)
        {
            if (hittingSFX.isPlaying == false)
            {
                hittingSFX.Play();
            }

            animator.ResetTrigger("GetDamage");
            animator.SetTrigger("GetDamage");
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
            dyingSFX.Play();
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

    public void PlayerResapwn(Transform pos)
    {
        playerController.enabled = true;

        currentHP = playerHP;
        animator.SetBool("IsIdle", true);

        controller.enabled = false;
        controller.transform.position = pos.position;
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

    public void GetSkull()
    {
        NumOfSkull++;
    }

    public bool CheckPlayerHasEverySkull()
    {
        if(NumOfSkull >= NumOfSkullInWorld)
        {
            GameObject.FindGameObjectWithTag("TreasureBox").GetComponent<TreasureBox>().SetHasEverySkull(true);
            return true;
        }

        return false;
    }

    public void HealingHP(int healing)
    {
        currentHP += healing;

        if (currentHP > 100)
            currentHP = 100;

    }

    public void CheatCodeForBossIsland()
    {
        NumOfSkull = NumOfSkullInWorld;
        bool checked_cheat_code = CheckPlayerHasEverySkull();

    }
}
