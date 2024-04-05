using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerActions inputActions;

    InputAction movementAction;
    InputAction cameraAction;

    private void Awake()
    {
        inputActions = new PlayerActions();
        movementAction = inputActions.Locomotion.Movement;
        cameraAction = inputActions.Locomotion.Camera;

        movementAction.performed += OnMove;
        movementAction.canceled += OnMove;
        cameraAction.performed += OnCam;
        cameraAction.canceled += OnCam;
    }

    private void OnEnable()
    {
        inputActions.Locomotion.Enable();
    }

    private void OnDisable()
    {
        movementAction.performed -= OnMove;
        movementAction.canceled -= OnMove;
        cameraAction.performed -= OnCam;
        cameraAction.canceled -= OnCam;

        inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        PlayerManager.instance.SetMoveInput(context.ReadValue<Vector2>());
    }

    private void OnCam(InputAction.CallbackContext context)
    {
        PlayerManager.instance.SetCamInput(context.ReadValue<Vector2>());
    }
}
