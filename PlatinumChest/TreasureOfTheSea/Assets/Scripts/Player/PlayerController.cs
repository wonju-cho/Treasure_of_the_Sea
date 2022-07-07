using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera & Character Syncing")]
    public float lookDistance = 5;
    public float lookSpeed = 5;
    Transform camCenter;

    [Header("Input Settings")]
    public string forwardInput = "Vertical";
    public string leftInput = "Horizontal"; 
    public string aim_input = "Fire1";
    public string fire_input = "Fire2";

    [Header("Aiming Settings")]
    public RaycastHit hit;
    public LayerMask aimLayers;
    public Ray ray;

    [Header("Spine Settings")]
    public Transform spine;
    public Vector3 spineOffset;

    [Header("Head Rotation Settings")]
    public float lookAtPoint = 2.8f;

    [Header("Character Controller")]
    private CharacterController controller;
    public Animator animator;

    public float moveSpeed = 6f;
    public float rotationSpeed = 6f;
    public float jumpSpeed = 2f;
    public float jumpButtonGracePeriod;
    public float roationSpeed = 3f;
    float turnSmoothVelocity;

    private float ySpeed;
    private float originalStepOffset; //to resolve the character glitch when jumping while colliding object
    
    private float? lastGroundedTime; //? means this field is nullable -> if the value is null then return null, otherwise return the value
    private float? jumpButtonPressedTime;

    private bool isJumping;
    private bool isGrounded;

    [Tooltip("Need to add playerhotbar object")]
    public GameObject InventoryUI;
    private bool isInventoryDisplayed = false;
    
    private Camera mainCamera;
    public Bow bowScript;

    public bool hitDetected;

    private Crafting crafting;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //inventoryImage.SetActive(false);

        InventoryUI = GameObject.FindWithTag("InventoryDisplay");

        originalStepOffset = controller.stepOffset;
        crafting = GameObject.FindWithTag("Crafting").GetComponent<Crafting>();

        if (!controller)
            Debug.Log("There is no controller in the PlayerMovement script");

        if (!animator)
            Debug.Log("There is no animator in the PlayerMovement script");

        mainCamera = Camera.main;
        camCenter = mainCamera.transform.parent;
    }

    void Update()
    {
        if (isInventoryDisplayed)
        {
            InventoryUI.SetActive(true);
            //inventoryImage.SetActive(true);
        }
        else
        {
            InventoryUI.SetActive(false);
            //inventoryImage.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryDisplayed = isInventoryDisplayed ? false : true;            
        }

        if (Input.GetAxis(forwardInput)!= 0 || Input.GetAxis(leftInput) !=0)
        {
            //RotateToCamView();
        }


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        float directionMagnitude = Mathf.Clamp01(direction.magnitude) * moveSpeed;
       
        direction = Quaternion.AngleAxis(mainCamera.transform.rotation.eulerAngles.y, Vector3.up) * direction;
        direction.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime; //gravity = -9.81
        
        if (controller.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }
        
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            controller.stepOffset = originalStepOffset;
        
            ySpeed = -0.5f; //keep character on the ground
        
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
        
            animator.SetBool("IsJumping", false);
            isJumping = false;
        
            animator.SetBool("IsFalling", false);
        
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                animator.SetBool("IsJumping", true);
                isJumping = true;
        
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            controller.stepOffset = 0;
        
            animator.SetBool("IsGrounded", false);
            isGrounded = false;
        
            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }
        
        Vector3 velocity = direction * directionMagnitude;
        velocity.y = ySpeed;
        
        controller.Move(velocity * Time.deltaTime);
        
        if (direction != Vector3.zero) //when moving
        {
            animator.SetBool("IsMoving", true);
        
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        
    }

   

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void PullArrow()
    {
        bowScript.PullString();
    }

    public void EnableArrow()
    {
        bowScript.PickArrow();
    }

    public void DisableArrow()
    {
        bowScript.DisableArrow();
    }

    public void ReleaseArrow()
    {
        bowScript.ReleaseString();
    }

    public void Aim()
    {
        Vector3 camPosition = mainCamera.transform.position;
        Vector3 direction = mainCamera.transform.forward;

        ray = new Ray(camPosition, direction);
        
        if (Physics.Raycast(ray, out hit, 500f, aimLayers))
        {
            hitDetected = true;
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            bowScript.ShowCrosshair(hit.point);
        }
        else
        {
            hitDetected = false;
            bowScript.ReMoveCrossHair();
        }
        
    }

    private void LateUpdate()
    {
        if (Input.GetButton(aim_input))
        {
            RotateCharacterSpine();
        }
    }
    public void RotateCharacterSpine()
    {
        spine.LookAt(ray.GetPoint(50));

        spine.Rotate(spineOffset);
    }

    void RotateToCamView()
    {
        Vector3 camCenterPos = camCenter.position;

        Vector3 lookPoint = camCenterPos + camCenter.forward * lookDistance;
        Vector3 direction = lookPoint - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        lookRotation.x = 0;
        lookRotation.z = 0;

        Quaternion final = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
        transform.rotation = final;
    }
}
