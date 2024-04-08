using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum CameraMode
{
    Normal,
    FreeForm
}

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform cameraLookTarget;

    [SerializeField]
    Transform cameraPositionTarget;

    CameraMode mode;

    [SerializeField]
    float maxRho;

    float phi, theta;

    [SerializeField]
    Camera cam;

    [SerializeField]
    LayerMask mask;

    private void Start()
    {
        mode = CameraMode.Normal;
    }

    private void FixedUpdate()
    {
        switch (mode)
        {
            case CameraMode.Normal:
                NormalFixedUpdate();
                break;
            case CameraMode.FreeForm:
                break;
        }
    }

    void NormalFixedUpdate()
    {
        Vector2 camInput = PlayerManager.instance.camInput;

        phi -= camInput.y * SETTINGS.SENSITIVITY;//SETTINGS.SENSITIVITY;
        phi = Mathf.Clamp(phi, 0.2f, Mathf.PI - 0.2f);
        theta -= camInput.x * SETTINGS.SENSITIVITY;//SETTINGS.SENSITIVITY;
        theta = theta % (2 * Mathf.PI);

        Vector3 dir = CartesianFromSphere(1, phi, theta);
        dir.Normalize();

        RaycastHit hit;

        if(Physics.Raycast(cameraLookTarget.position, dir, out hit, maxRho, mask))
        {
            cameraPositionTarget.position = cameraLookTarget.position + CartesianFromSphere(hit.distance, phi, theta) + (hit.normal * 0.5f);
        }
        else
        {
            cameraPositionTarget.position = cameraLookTarget.position + CartesianFromSphere(maxRho, phi, theta);
        }
        cam.transform.position = Vector3.Lerp(cam.transform.position, cameraPositionTarget.position, 0.5f);

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1f, mask))
        {
            transform.position += new Vector3(0, 1f, 0);
        }

        cam.transform.LookAt(cameraLookTarget);

        Vector3 cameraForwardVector = cam.transform.forward;
        cameraForwardVector.y = 0f;

        PlayerManager.instance.SetCameraForwardVector(cameraForwardVector);
    }

    Vector3 CartesianFromSphere(float rho, float phi, float theta) 
    {
        return new Vector3(
            Mathf.Sin(phi) * Mathf.Cos(theta),
            Mathf.Cos(phi),
            Mathf.Sin(phi) * Mathf.Sin(theta)) * rho;
    }
}
