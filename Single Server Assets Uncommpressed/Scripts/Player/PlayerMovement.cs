using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The Player Controller")]
    public CharacterController playerController;
    [Tooltip("The Child Cam of this Object")]
    public Camera mainCam;
    [Tooltip("Move Speed With No Run")]
    public float moveSpeed = 5;
    [Tooltip("Movement Speed While Running")]
    public float runSpeed = 8;
    [Tooltip("The Max Amount of Staminia")]
    public float maxStamina = 10;
    [Tooltip("The Remaining Staminia")]
    public float currentStamina = 10;
    [Tooltip("The Sensitivity of The Mouse")]
    public float mouseSensitivity = 500;
    [Tooltip("The Gravity Force")]
    public float gravity = -9.81f;
    [Tooltip("How Small The Player Should Get While Crouching")]
    public float crouchHeight;

    [Tooltip("The Size of The Sphere when Checking for Ground")]
    public float groundDistance = 0.1f;
    [Tooltip("The Force of The Jump")]
    public float jumpHeight = 3;
    [Tooltip("The Object Where To Check for Ground")]
    public GameObject groundCheck;
    [Tooltip("The Objects That Are Ground")]
    public LayerMask groundMask;

    [Header("Should Use")]
    public bool shouldUseStaminia = true;
    public bool hasRun = true;
    public bool hasGravity = true;
    public bool canCrouch = true;
    public bool shouldUseJump = true;
    //public bool hasAirStrafe = true;
    [Header("Key Binds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode runKey = KeyCode.LeftShift;

    private Vector3 velocity;
    private bool isGrounded;
    private bool canRun = true;
    private float xRotation;
    private float startPlayerHeight;

    private float xVector;
    private float zVector;

    public KeyboardActions keyboardControls;

    void Awake()
    {
        keyboardControls = new KeyboardActions();
        playerController = GetComponent<CharacterController>();
        if (shouldUseStaminia == true)
            currentStamina = maxStamina;

        startPlayerHeight = playerController.height;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -3f;
        }

        xVector = keyboardControls.Controls.Movement.ReadValue<Vector2>().x;
        zVector = keyboardControls.Controls.Movement.ReadValue<Vector2>().y;

        Vector3 move = transform.right * xVector + transform.forward * zVector;
        playerController.Move(move * moveSpeed * Time.deltaTime);

        if (Keyboard.current.leftShiftKey.isPressed && canRun == true && hasRun == true)
        {
            playerController.Move(move * runSpeed * Time.deltaTime);
            if (shouldUseStaminia == true)
                currentStamina -= Time.deltaTime;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && shouldUseJump)
            Jump();

        if (Keyboard.current.ctrlKey.isPressed && canCrouch)
        {
            playerController.height = crouchHeight;
        }

        else
            playerController.height = startPlayerHeight;

        if (currentStamina <= 0)
        {
            canRun = false;
            StartCoroutine(GiveStaminia());
        }

        if (currentStamina > 0)
        {
            canRun = true;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (hasGravity == true)
            velocity.y += gravity * Time.deltaTime;

        playerController.Move(velocity * Time.deltaTime);

        float mouseX = keyboardControls.Controls.Turning.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = keyboardControls.Controls.Turning.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 65f);

        mainCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }


    void Jump()
    {
        if (isGrounded == true)
        {
            velocity.y = jumpHeight;
            velocity.y -= gravity * Time.deltaTime;
            playerController.Move(velocity * Time.deltaTime);
        }
    }

    IEnumerator GiveStaminia()
    {
        yield return new WaitForSeconds(3);
        currentStamina += Time.deltaTime;
    }
}
