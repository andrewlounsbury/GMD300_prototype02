using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    public float MaxMoveSpeed = 5;
    public float MoveAcceleration = 10;
    public float JumpSpeed = 5;
    public float JumpMaxTime = 0.5f;
    public Camera PlayerCamera;

    private float JumpTimer = 0; 

    private CharacterController characterController;
    private bool jumpInputPressed = false;
    private bool isJumping = false; 
    private Vector2 moveInput = Vector2.zero;
    private Vector2 currentHorizontalVelocity = Vector2.zero; 
    private float currentVerticalVelocity = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 cameraSpaceMovement = new Vector3(moveInput.x, 0, moveInput.y); 
        cameraSpaceMovement = PlayerCamera.transform.TransformDirection(cameraSpaceMovement);
        
        Vector2 cameraHorizontalMovmement = new Vector2(cameraSpaceMovement.x, cameraSpaceMovement.z);

        currentHorizontalVelocity = Vector2.Lerp(currentHorizontalVelocity, cameraHorizontalMovmement * MaxMoveSpeed, Time.deltaTime * MoveAcceleration); 

        if (isJumping == false)
        {
            currentVerticalVelocity += Physics.gravity.y * Time.deltaTime; 

            if(characterController.isGrounded && currentVerticalVelocity < 0)
            {
                currentVerticalVelocity = Physics.gravity.y * Time.deltaTime;
            }
        }
        else
        {
            JumpTimer += Time.deltaTime;

            if (JumpTimer >= JumpMaxTime)
            {
                isJumping = false; 
            }
        }
        Vector3 currentVelocity = new Vector3(currentHorizontalVelocity.x, currentVerticalVelocity, currentHorizontalVelocity.y);

        Vector3 horizontalDirection = Vector3.Scale(currentVelocity,  new Vector3(1, 0, 1));

        if (horizontalDirection.magnitude > 0.0001) 
        {
            Quaternion newDirection = Quaternion.LookRotation(horizontalDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * MoveAcceleration); 
        }

        characterController.Move(currentVelocity * Time.deltaTime); 
    }

    public void OnMove(InputValue value)
    {

        
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        jumpInputPressed = value.Get<float>() > 0; 

        if(jumpInputPressed)
        {
            if (characterController.isGrounded)
            {
                isJumping = true;
                JumpTimer = 0; 
                currentVerticalVelocity = JumpSpeed;
            }
        }
        else
        {
            if (isJumping)
            {
                isJumping = false;
            }
        }
    }

    public void OnAttack(InputValue value)
    {
        Collider[] overlapItems = Physics.OverlapBox(transform.position, Vector3.one);
        
        if (overlapItems.Length > 0)
        {
            foreach (Collider item in overlapItems)
            {
                Vector3 direction = item.transform.position - transform.position;
                item.SendMessage("OnPlayerAttack", direction, SendMessageOptions.DontRequireReceiver); 
            }
        }
    }

}
