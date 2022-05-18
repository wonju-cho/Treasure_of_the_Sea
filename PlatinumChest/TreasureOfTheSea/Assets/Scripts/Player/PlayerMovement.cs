using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController Controller;

    public float MoveSpeed = 6f;
    public float RotationSpeed = 6f;
    public float JumpSpeed = 2f;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        
        if (!Controller)
            Debug.Log("There is no controller in the PlayerMovement script");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Vector3 Direction = new Vector3(Horizontal, 0f, Vertical);
        float DirectionMagnitude = Mathf.Clamp01(Direction.magnitude) * MoveSpeed;
        Direction.Normalize();

        Controller.SimpleMove(Direction * DirectionMagnitude);

        if (Direction != Vector3.zero)
        {
            //Quaternion rot = new Quaternion();
            //rot.SetLookRotation(Direction);
            //transform.rotation = rot;

            Quaternion ToRotation = Quaternion.LookRotation(Direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, ToRotation, RotationSpeed * Time.deltaTime);
        }
    }


}
