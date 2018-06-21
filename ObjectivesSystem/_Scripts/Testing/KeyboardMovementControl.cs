using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovementControl : MonoBehaviour {

    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    private float speed = 2F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        speed = (Input.GetKey(KeyCode.LeftShift)) ? runSpeed : walkSpeed;
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKey(KeyCode.Space))
                moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

}
