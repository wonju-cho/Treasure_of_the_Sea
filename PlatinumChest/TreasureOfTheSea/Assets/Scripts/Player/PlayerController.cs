using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

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
    
    //private bool isPlayerDead = false;
    //private bool isPlayerDeadSea = false;
    //private bool SeaTriggerOnce = false;

    //Vector3 playerSpawnPosition;

    //[SerializeField]
    //int PlayerHP = 10;

    //private int currentHP;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        if (!controller)
            Debug.Log("There is no controller in the PlayerMovement script");

        if (!animator)
            Debug.Log("There is no animator in the PlayerMovement script");


        //playerSpawnPosition = transform.position;
        //currentHP = PlayerHP;
        
        originalStepOffset = controller.stepOffset;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentHP);


        //if (currentHP <= 0)
        //{
        //    isPlayerDead = true;
        //}

        //if (isPlayerDead)
        //{
        //    PlayerDeath();  
        //}

        //if (!isPlayerDead)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical);
            float directionMagnitude = Mathf.Clamp01(direction.magnitude) * moveSpeed;
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
    
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if(hit.gameObject.CompareTag("Sea"))
    //    {
    //        isPlayerDead = true;
    //        isPlayerDeadSea = true;
    //    }
    //}

    //void PlayerDeath()
    //{
    //    if(isPlayerDeadSea && SeaTriggerOnce == false)
    //    {
    //        SeaTriggerOnce = true;
    //        animator.SetTrigger("Death");
    //        Debug.Log(isPlaying(animator, "Death"));
    //    }
    //    else if(isPlayerDead && isPlayerDeadSea == false)
    //    {
    //        animator.SetTrigger("Death");
    //        Debug.Log(isPlaying(animator, "Death"));
    //    }
    //}

    //bool isPlaying(Animator anim, string stateName)
    //{
    //    if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
    //            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
    //        return true;
    //    else
    //        return false;
    //}

    IEnumerator WaitForSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    //void PlayerRespawn()
    //{
    //    controller.enabled = false;
    //    controller.transform.position = playerSpawnPosition;
    //    controller.enabled = true;

    //    isPlayerDead = false;
    //    isPlayerDeadSea = false;
    //    SeaTriggerOnce = false;

    //    animator.ResetTrigger("Death");
    //}
}
