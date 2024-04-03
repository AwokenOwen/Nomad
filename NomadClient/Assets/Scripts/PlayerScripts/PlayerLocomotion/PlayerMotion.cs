using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    RaycastHit hit;

    [Header("Movement Variables")]
    Vector3 totalAccel;

    [SerializeField]
    float terminalVelocity;

    [SerializeField]
    Vector3 gravityAccel;

    [SerializeField]
    float gravityAngleOffset;

    [SerializeField]
    float baseMoveSpeed;

    private void FixedUpdate()
    {
        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 0.01f))
        {
            transform.up = hit.normal;
        }

        Vector2 inputVec = PlayerManager.instance.GetMoveVec();

        Vector3 inputDirectionalVector = transform.forward * inputVec.y + transform.right * inputVec.x;

        Vector3 gravity = PlayerManager.instance.getGravity();

        Vector3 movement = Vector3.ProjectOnPlane((inputDirectionalVector * baseMoveSpeed) + gravity, hit.normal);

        if (Physics.SphereCast(transform.position, 0.5f, movement.normalized, out hit, movement.magnitude))
        {
            Debug.Log("hit");
            transform.up = hit.normal;
            movement = Vector3.ProjectOnPlane(movement, hit.normal);
        }

        transform.position += movement;
    }
}
