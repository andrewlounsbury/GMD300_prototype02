using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    //public movement variables 
    public float MaxMoveSpeed = 5;
    public float MoveAcceleration = 10;
    public float JumpSpeed = 5;
    public float JumpMaxTime = 0.5f;
    public bool canControl = true; 

    //public camera object 
    public Camera PlayerCamera;

    //public block animations/objects/variables 
    //add block anim here
    public Slider BlockSlider;
    public Image BlockFill; 
    public float BlockStamina, MaxBlockStamina;

    //private attack animations/variables 
    [SerializeField] private SwordAnimation SwordAnimation;
    [SerializeField] private bool Attack1;
    [SerializeField] private bool Attack2;
    [SerializeField] private bool Attack3;
    private float currentAttackCombo = 0;
    private float maxAttackCombo = 3;

    //private objects/variables 
    private float JumpTimer = 0; 
    private CharacterController characterController;
    private bool jumpInputPressed = false;
    private bool isJumping = false;
    private bool blockPressed; 
    private Vector2 moveInput = Vector2.zero;
    private Vector2 currentHorizontalVelocity = Vector2.zero; 
    private float currentVerticalVelocity = 0;
    private bool recharging = false;
    private Coroutine recharge; 

    private void Awake()
    {
        //calls character controller object on object awake to recieve player inputs
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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


            if (blockPressed)
            {
                BlockStamina -= Time.deltaTime;

               if (BlockStamina < 0)
               {
                 BlockStamina = 0;
               }

                BlockSlider.value = BlockStamina;
            }

            if (recharging)
            {
                BlockStamina += Time.deltaTime;
                if (BlockStamina > MaxBlockStamina)
                {
                    BlockStamina = MaxBlockStamina;
                    recharging = false;
                }

                BlockSlider.value = BlockStamina;

            }
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
        SwordAnimation.TriggerAttackCombo();
    }

    public void OnBlock(InputValue value)
    {
        blockPressed = value.isPressed; 

        if (!blockPressed)
        {
            StartCoroutine(RechargeStamina());
        } 
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        recharging = true;
        Debug.Log(recharging);
    }

}