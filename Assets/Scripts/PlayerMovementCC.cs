using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour
{   
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    float cameraX;
    float cameraY;
    float xRotation = 0f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private float maxLookAngle = 90f;
 

    [Header("References")]
    private Rigidbody rb;
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotateCamera();
    }
    private void MyInput()
    {
        // mouse input
        cameraX = Input.GetAxis("Mouse X") * rotationSpeed;
        cameraY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // movement input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection.y = 0.0f;

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void RotateCamera()
    {
        // rotate the child that contains the camera
        transform.Rotate(Vector3.up * cameraX);

        xRotation -= cameraY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
