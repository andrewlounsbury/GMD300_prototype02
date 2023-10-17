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
    public bool canControl = true; 
    public Camera PlayerCamera;

    [SerializeField] private SwordAnimation SwordAnimation;
    [SerializeField] private bool Attack1;
    [SerializeField] private bool Attack2;
    [SerializeField] private bool Attack3; 
    private float JumpTimer = 0; 
    private CharacterController characterController;
    private bool jumpInputPressed = false;
    private bool isJumping = false; 
    private Vector2 moveInput = Vector2.zero;
    private Vector2 currentHorizontalVelocity = Vector2.zero; 
    private float currentVerticalVelocity = 0;

    private float currentAttackCombo = 0;
    private float maxAttackCombo = 3;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (canControl)
        {
            /*Attack1 = SwordAnimation.GetComponent<Animator>().GetBool("Attack1");
            Attack2 = SwordAnimation.GetComponent<Animator>().GetBool("Attack2");
            Attack3 = SwordAnimation.GetComponent<Animator>().GetBool("Attack3");*/

            Vector3 cameraSpaceMovement = new Vector3(moveInput.x, 0, moveInput.y);
            cameraSpaceMovement = PlayerCamera.transform.TransformDirection(cameraSpaceMovement);

            Vector2 cameraHorizontalMovmement = new Vector2(cameraSpaceMovement.x, cameraSpaceMovement.z);

            currentHorizontalVelocity = Vector2.Lerp(currentHorizontalVelocity, cameraHorizontalMovmement * MaxMoveSpeed, Time.deltaTime * MoveAcceleration);



            if (isJumping == false)
            {
                currentVerticalVelocity += Physics.gravity.y * Time.deltaTime;

                if (characterController.isGrounded && currentVerticalVelocity < 0)
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

            Vector3 horizontalDirection = Vector3.Scale(currentVelocity, new Vector3(1, 0, 1));

            if (horizontalDirection.magnitude > 0.0001)
            {
                Quaternion newDirection = Quaternion.LookRotation(horizontalDirection.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * MoveAcceleration);
            }

            characterController.Move(currentVelocity * Time.deltaTime);
        }
        
    }

    public void OnMove(InputValue value)
    {
        SwordAnimation.WalkAnim(true); 
        
        moveInput = value.Get<Vector2>();

        if (moveInput == new Vector2(0, 0) ) 
        {
            SwordAnimation.WalkAnim(false); 
        }
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
        /*if(value.Get<float>() > 0) 
        {

            SwordAnimation.AttackAnim1(true);
            if(Attack1 && value.Get<float>() > 0)
            {
                SwordAnimation.AttackAnim2(true);
                SwordAnimation.AttackAnim1(false);

                if (Attack2 && value.Get<float>() > 0)
                {
                    SwordAnimation.AttackAnim3(true);
                    SwordAnimation.AttackAnim2(false);

                    if (Attack2 && value.Get<float>() > 0)
                    {
                        SwordAnimation.AttackAnim3(false);
                    }
                }
            }
        }*/

        SwordAnimation.TriggerAttackCombo();
    }

}
