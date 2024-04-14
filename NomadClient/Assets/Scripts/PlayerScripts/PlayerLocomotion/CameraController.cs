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

    [SerializeField]
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

    private void Update()
    {
        switch (mode)
        {
            case CameraMode.Normal:
                NormalUpdate();
                break;
            case CameraMode.FreeForm:
                break;
        }
    }

    void NormalUpdate()
    {
        cam.transform.position = cameraPositionTarget.position;

        cam.transform.LookAt(cameraLookTarget);
    }

    void NormalFixedUpdate()
    {
        Vector2 camInput = PlayerManager.instance.camInput;

        phi += camInput.y * SETTINGS.SENSITIVITY;
        phi = Mathf.Clamp(phi, 0.2f, Mathf.PI - 0.2f);
        theta -= camInput.x * SETTINGS.SENSITIVITY;
        theta = theta % (2 * Mathf.PI);

        if (theta < 0)
            theta += 2 * Mathf.PI;

        Vector3 dir = CartesianFromSphere(1, phi, theta);
        dir.Normalize();

        RaycastHit hit;

        if (Physics.SphereCast(cameraLookTarget.position, 0.31f, dir, out hit, maxRho + 1f, mask))
        {
            cameraPositionTarget.position = cameraLookTarget.position + (dir * Mathf.Max(0.01f, (hit.distance - 0.1f)));
        }
        else
        {
            cameraPositionTarget.position = cameraLookTarget.position + CartesianFromSphere(maxRho, phi, theta);
        }

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
