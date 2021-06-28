using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask, sphereMask;

    Vector3 velocity;
    bool isGrounded;
    void Update()
    {
        Time.timeScale = 1f;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Physics.CheckSphere(groundCheck.position, groundDistance, sphereMask))
        {
            Time.timeScale = 2f;
            gravity = -4.405f;
        }

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //gameObject.GetComponent<Rigidbody>().AddForce(move * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
