using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody RB;
    [Space] 
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float RotateSpeed = 3f;
    [SerializeField] private float JumpForce = 2f;

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;

    public Transform GroundCheck;
    public bool IsGrounded;

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * MoveSpeed;
        RB.velocity = new Vector3(MoveVector.x, RB.velocity.y, MoveVector.z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

        }
    }
}
