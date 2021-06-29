using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField]
    float desiredGravity = -25f;
    [SerializeField]
    float lowerGravityValue = -4.905f;
    [SerializeField]
    float MovementLerpOnGround;
    [SerializeField]
    float MovementLerpInAir;

    public float speed = 12f;
    float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask, sphereMask;

    Vector3 velocity;
    bool isGrounded, inArea;
    float setTimeScale;

    float x = 0, z = 0;

    private void Start()
    {
        gravity = desiredGravity;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            //if(!inArea) 
            //Time.timeScale = 1f;
            //velocity.y = -2f;
            gravity = -25f;
            x = Mathf.Lerp(x, Input.GetAxis("Horizontal"), Time.deltaTime * MovementLerpOnGround);
            z = Mathf.Lerp(z, Input.GetAxis("Vertical"), Time.deltaTime * MovementLerpOnGround);
        } 
        else
        {
            x = Mathf.Lerp(x, Input.GetAxis("Horizontal"), Time.deltaTime * MovementLerpInAir);
            z = Mathf.Lerp(z, Input.GetAxis("Vertical"), Time.deltaTime * MovementLerpInAir);
        }

        if(!inArea)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.deltaTime);
            gravity = Mathf.Lerp(gravity, -25f, Time.deltaTime);
        }

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
            /*
            inArea = true;
            Time.timeScale = 2f;
            setTimeScale = 2f;
            gravity = lowerGravityValue;*/
            speed *= 2f;
        } 
        else if(other.tag == "Slow")
        {
            inArea = true;
            Time.timeScale = .5f;
            setTimeScale = .5f;
            gravity = lowerGravityValue;
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
