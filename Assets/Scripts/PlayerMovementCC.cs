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
    private float groundRaycastRange = 1.01f;

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
        groundRaycastRange = transform.localScale.y + 0.02f;
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
        SpeedControl();
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

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Impulse);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void RotateCamera()
    {
        // rotate the child that contains the camera
        transform.Rotate(Vector3.up * cameraX);

        xRotation -= cameraY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundRaycastRange);
    }
}
