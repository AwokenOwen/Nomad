using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    Vector3 inputForward;


    private void Update()
    {   
        //Get Input
        Vector3 input = PlayerManager.instance.moveInput;

        Vector3 cameraForward = PlayerManager.instance.cameraForwardVector;

        inputForward = new Vector3();

        if (PlayerManager.instance.grounded)
        {
            inputForward = cameraForward * input.y + Vector3.Cross(cameraForward, Vector3.up) * -input.x;
        }

        if (PlayerManager.instance.OnSlope() && !PlayerManager.instance.exitingSlope)
        {
            if (PlayerManager.instance.rb.velocity.magnitude > GameManager.instance.currentWorldData.GetMoveSpeed())
            {
                PlayerManager.instance.rb.velocity = PlayerManager.instance.rb.velocity.normalized * GameManager.instance.currentWorldData.GetMoveSpeed();
            }
        }
        else
        {
            //get horizontal vel
            Vector3 horizontalVel = new Vector3(PlayerManager.instance.rb.velocity.x, 0f, PlayerManager.instance.rb.velocity.z);

            //cap movespeed
            if (horizontalVel.magnitude > GameManager.instance.currentWorldData.GetMoveSpeed())
            {
                horizontalVel = horizontalVel.normalized * GameManager.instance.currentWorldData.GetMoveSpeed();
                PlayerManager.instance.rb.velocity = new Vector3(horizontalVel.x, PlayerManager.instance.rb.velocity.y, horizontalVel.z);
            }
        }
        //check if normal or no drag should be used
        if (PlayerManager.instance.grounded)
            PlayerManager.instance.rb.drag = PlayerManager.instance.groundDrag;
        else
            PlayerManager.instance.rb.drag = 0f;
    }

    private void FixedUpdate()
    {
        RaycastHit slopeHit;
        if (PlayerManager.instance.OnSlope(out slopeHit) && !PlayerManager.instance.exitingSlope)
        {
            MoveCharacterSlope(Vector3.ProjectOnPlane(inputForward, slopeHit.normal));
        }
        else
        {
            //if capsule cast toward direction add force in direction of project on plane with normal with y = 0f; 
            Vector3 point1 = transform.position + (Vector3.up * ((PlayerManager.instance.playerHeight / 2f)));
            Vector3 point2 = transform.position + (Vector3.down * ((PlayerManager.instance.playerHeight / 2f)));

            RaycastHit hit;
            if (Physics.CapsuleCast(point1, point2, 0.25f, inputForward, out hit, 0.3f, PlayerManager.instance.groundMask))
            {
                Vector3 newNormal = hit.normal;
                newNormal.y = 0f;
                newNormal.Normalize();
                MoveCharacter(Vector3.ProjectOnPlane(inputForward, newNormal));
            }
            else
            {
                MoveCharacter(inputForward);
            }
        }

        PlayerManager.instance.rb.useGravity = !PlayerManager.instance.OnSlope();
    }

    private void MoveCharacter(Vector3 dir)
    {
        if (PlayerManager.instance.grounded)
            PlayerManager.instance.rb.AddForce(dir * GameManager.instance.currentWorldData.GetMoveSpeed() * 10f, ForceMode.Force);
        else
            PlayerManager.instance.rb.AddForce(dir * GameManager.instance.currentWorldData.GetMoveSpeed() * 10f * PlayerManager.instance.airMultiplier, ForceMode.Force);
    }

    private void MoveCharacterSlope(Vector3 dir)
    {
        PlayerManager.instance.rb.AddForce(dir * GameManager.instance.currentWorldData.GetMoveSpeed() * 15f, ForceMode.Force);

        if (Mathf.Abs(PlayerManager.instance.rb.velocity.y) > 0)
            PlayerManager.instance.rb.AddForce(Vector3.down * 80f, ForceMode.Force);
    }
}
