using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController Controller;

    public float MoveSpeed = 6f;
    public float RotationSpeed = 6f;
    public float JumpSpeed = 2f;

    private float ySpeed;
    private float originalStepOffset;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        
        if (!Controller)
            Debug.Log("There is no controller in the PlayerMovement script");

        originalStepOffset = Controller.stepOffset;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Vector3 Direction = new Vector3(Horizontal, 0f, Vertical);
        float DirectionMagnitude = Mathf.Clamp01(Direction.magnitude) * MoveSpeed;
        Direction.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime; //gravity = -9.81

        if(Controller.isGrounded)
        {
            Controller.stepOffset = originalStepOffset;

            ySpeed = -0.5f; //keep character on the ground

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = JumpSpeed;
            }
        }
        else
        {
            Controller.stepOffset = 0;
        }


        Vector3 velocity = Direction * DirectionMagnitude;
        velocity.y = ySpeed;

        Controller.Move(velocity * Time.deltaTime);

        if (Direction != Vector3.zero)
        {

            Quaternion ToRotation = Quaternion.LookRotation(Direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, ToRotation, RotationSpeed * Time.deltaTime);
        }
    }


}
