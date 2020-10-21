using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    float camStartingY;
    float startingHeight;
    bool isCrouching;
    public float crouchDistance;
    public bool isDisabled = false;
    public bool isGrounded;
    public float moveSpeed;
    public float gravity = -9.81f;
    public bool isMoving;
    public bool canPlayStep;
    float timeSinceLastStep;
    Vector3 velocity;

    void Awake()
    {
        startingHeight = controller.height;
        camStartingY = Camera.main.transform.position.y;
    }

    void Update()
    {
        if(!isDisabled)
        {
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if(isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }
                float xMove = Input.GetAxis("Horizontal");
                float zMove = Input.GetAxis("Vertical");

                Vector3 move = transform.right * xMove + transform.forward * zMove;

                if(move.x > 0 || move.y > 0)
                {
                    if(!isMoving)
                        isMoving = true;
                }
                else
                {
                    if(isMoving)
                        isMoving = false;
                }

                controller.SimpleMove(move * moveSpeed);

                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);

                
                if(Input.GetKeyDown(KeyCode.LeftControl))
                {
                    if(!isCrouching)
                    {
                        Crouch();
                    }
                }
                if(Input.GetKeyUp(KeyCode.LeftControl))
                {
                    if(isCrouching)
                    {
                        ReleaseCrouch();
                    }
                }
        }
    }

    void Crouch()
    {
        //set controller height and camera height lower
        isCrouching = true;
        controller.height = crouchDistance;
        
    }

    void ReleaseCrouch()
    {
        isCrouching = false;
        controller.height = startingHeight;
    }
}
