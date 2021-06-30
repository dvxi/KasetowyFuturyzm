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
    float desiredSpeed = 12f;
    [SerializeField]
    float MovementLerpOnGround;
    [SerializeField]
    float MovementLerpInAir;
    [SerializeField]
    float BoostLerp;

    [SerializeField]
    TrailRenderer SpeedParticles;
    [SerializeField]
    Color boostColor;
    [SerializeField]
    Color slowColor;

    public float speed = 12f;
    float gravity = -9.81f;
    public float jumpHeight = 3f;
    float movementLerpSpeed;

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
        speed = desiredSpeed;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Mathf.Abs(speed - desiredSpeed) <= 0.05f) //normal speed reached
        {
            SpeedParticles.emitting = false;
        }

        if (isGrounded && velocity.y < 0) movementLerpSpeed = MovementLerpOnGround; //delay of movement vector change
        else movementLerpSpeed = MovementLerpInAir;

        x = Mathf.Lerp(x, Input.GetAxis("Horizontal"), Time.deltaTime * movementLerpSpeed);
        z = Mathf.Lerp(z, Input.GetAxis("Vertical"), Time.deltaTime * movementLerpSpeed);

        if (!inArea)
        {
            gravity = Mathf.Lerp(gravity, desiredGravity, Time.deltaTime * BoostLerp); //lerp gravity and speed values to normal
            speed = Mathf.Lerp(speed, desiredSpeed, Time.deltaTime * BoostLerp);
        }

        Vector3 move = transform.right * x + transform.forward * z; //set move vector

        controller.Move(move * speed * Time.deltaTime); //move

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

            gravity = lowerGravityValue;
            speed *= 2f;
            SpeedParticles.emitting = true;
            SpeedParticles.startColor = boostColor;
        } 
        else if(other.tag == "Slow")
        {
            inArea = true;

            gravity = lowerGravityValue;
            speed *= .5f;
            SpeedParticles.emitting = true;
            SpeedParticles.startColor = slowColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boost" || other.tag == "Slow") inArea = false;
    }
}
