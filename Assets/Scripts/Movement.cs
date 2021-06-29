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
    bool isGrounded, inArea;
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0 && !inArea)
        {
            Time.timeScale = 1f;
            velocity.y = -2f;
            gravity = -25f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * -9.81f);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boost")
        {
            inArea = true;
            Time.timeScale = 2f;
            gravity = -4.905f;
        } 
        else if(other.tag == "Slow")
        {
            inArea = true;
            Time.timeScale = .5f;
            gravity = -4.905f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boost" || other.tag == "Slow") inArea = false;
        if (other.tag == "Slow")
        {
            Time.timeScale = 1f;
            velocity.y = -2f;
            gravity = -25f;
        }
    }
}
