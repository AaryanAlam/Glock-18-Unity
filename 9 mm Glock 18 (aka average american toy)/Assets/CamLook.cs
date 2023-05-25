using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{
    public float sensitivity = 2f;  // Mouse sensitivity

    private float xRotation = 0f;  // Current rotation around the X-axis
    private float yRotation = 0f;  // Current rotation around the Y-axis

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // Read mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the camera horizontally and vertically
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limit vertical rotation to -90 to 90 degrees

        // Apply rotations to the camera
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
