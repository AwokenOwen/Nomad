using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float moveSpeed, maxSpeed;

    [SerializeField]
    float groundDrag;

    [SerializeField]
    float playerHeight;
    [SerializeField]
    LayerMask ground;
    [SerializeField]
    bool grounded;

    Vector3 inputForward;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;
        grounded = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, (playerHeight * 0.5f) + 0.2f, ground);

        Vector3 input = PlayerManager.instance.GetMoveInput();

        Vector3 cameraForward = PlayerManager.instance.GetCameraForwardVector();

        inputForward = cameraForward * input.y + Vector3.Cross(cameraForward, Vector3.up) * -input.x;

        Vector3 horizontalVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (horizontalVel.magnitude > maxSpeed)
        {
            horizontalVel = horizontalVel.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVel.x, rb.velocity.y, horizontalVel.z);
        }

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;
    }

    private void FixedUpdate()
    {
        rb.AddForce(inputForward * moveSpeed * 10f); //add other stat stuff here
    }
}
