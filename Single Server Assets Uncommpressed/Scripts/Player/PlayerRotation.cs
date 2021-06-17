using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    public float mouseSensitivity = 5f;
    public float xRotation = 0f;

    public Transform playerObject;

    public KeyboardActions keyboardControls;

    void Awake()
    {
        keyboardControls = new KeyboardActions();
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
        float mouseX = keyboardControls.Controls.Turning.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = keyboardControls.Controls.Turning.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85, 65f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerObject.Rotate(Vector3.up * mouseX);
    }
}
