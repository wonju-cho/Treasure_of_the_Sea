using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Animator animator;

    public float moveSpeed = 6f;
    public float rotationSpeed = 6f;
    public float jumpSpeed = 2f;
    public float jumpButtonGracePeriod;

    private float ySpeed;
    private float originalStepOffset; //to resolve the character glitch when jumping while colliding object
    
    private float? lastGroundedTime; //? means this field is nullable -> if the value is null then return null, otherwise return the value
    private float? jumpButtonPressedTime;

    private bool isJumping;
    private bool isGrounded;

    public GameObject InventoryUI;
    private bool isInventoryDisplayed = false;
    
    private Camera mainCamera;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        originalStepOffset = controller.stepOffset;

        if (!controller)
            Debug.Log("There is no controller in the PlayerMovement script");

        if (!animator)
            Debug.Log("There is no animator in the PlayerMovement script");

        mainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        if (isInventoryDisplayed)
        {
            InventoryUI.SetActive(true);
        }
        else
        {
            InventoryUI.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInventoryDisplayed = isInventoryDisplayed ? false : true;
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
        
        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            if (animator.GetBool("IsShooting") == false)
            {
                animator.SetBool("IsShooting", true);
            }
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

}
