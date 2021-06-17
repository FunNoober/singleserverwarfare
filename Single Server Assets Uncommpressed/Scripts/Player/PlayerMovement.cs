using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float runSpeed = 11f;

    public float gravity = -9.81f;
    public float groundDistance = 0.5f;
    public float stamina = 10f;
    public float maxStamina = 10f;

    public float defaultMoveSpeed;
    public float defaultRunSpeed;

    Vector3 velocity;
    public Transform groundCheck;

    public CharacterController charController;
    public LayerMask groundMask;

    bool isGrounded;
    public bool isRunning;
    public bool canRun = true;

    public float jumpHeight = 10;

    public bool isController = false;

    public KeyboardActions keyboardControls;

    float xVector;
    float zVector;
    void Awake()
    {
        defaultMoveSpeed = moveSpeed;
        defaultRunSpeed = runSpeed;

        keyboardControls = new KeyboardActions();
    }

    void OnEnable()
    {
        keyboardControls.Enable();
    }

    void OnDisable()
    {
        keyboardControls.Disable();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -3f;
        }
        xVector = keyboardControls.Controls.Movement.ReadValue<Vector2>().x;
        zVector = keyboardControls.Controls.Movement.ReadValue<Vector2>().y;
        
        Vector3 move = transform.right * xVector + transform.forward * zVector;
        charController.Move(move * moveSpeed * Time.deltaTime);


        if (Keyboard.current.leftShiftKey.isPressed && canRun == true)
        {
            charController.Move(move * runSpeed * Time.deltaTime);
            stamina -= Time.deltaTime;
        }

        if(Keyboard.current.spaceKey.wasPressedThisFrame)
            Jump();

        if (stamina <= 0)
        {
            canRun = false;
            StartCoroutine(GiveStamina());
        }

        if (stamina >= maxStamina)
        {
            canRun = true;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina - 1);

        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            charController.height = 0.5f;
        }

        else
        {
            charController.height = 2f;
            moveSpeed = defaultMoveSpeed;
            runSpeed = defaultRunSpeed;
        }
    }

    void Jump()
    {
        if (isGrounded == true)
        {
            velocity.y = jumpHeight;
            velocity.y -= gravity * Time.deltaTime;
            charController.Move(velocity * Time.deltaTime);
        }
    }

    IEnumerator GiveStamina()
    {
        yield return new WaitForSeconds(4);
        stamina += 1f;
    }

}
