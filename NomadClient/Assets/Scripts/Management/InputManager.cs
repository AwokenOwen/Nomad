using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputMode
{
    Game,
    Menu
}

public class InputManager : MonoBehaviour
{
    PlayerActions inputActions;

    InputAction movementAction;
    InputAction cameraAction;
    InputAction jumpAction;

    private void Awake()
    {

        inputActions = new PlayerActions();

        //Locomotion inputs
        movementAction = inputActions.Locomotion.Movement;
        cameraAction = inputActions.Locomotion.Camera;
        jumpAction = inputActions.Locomotion.Jump;

        movementAction.performed += OnMove;
        movementAction.canceled += OnMove;
        cameraAction.performed += OnCam;
        cameraAction.canceled += OnCam;
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJump;

        //Menu inputs
    }

    private void OnEnable()
    {
        inputActions.Locomotion.Enable();
    }

    private void OnDisable()
    {
        //Locomotion inputs
        movementAction.performed -= OnMove;
        movementAction.canceled -= OnMove;
        cameraAction.performed -= OnCam;
        cameraAction.canceled -= OnCam;
        jumpAction.performed -= OnJump;
        jumpAction.canceled -= OnJump;

        //Menu inputs


        Disable();
    }

    private void Disable()
    {
        inputActions.Locomotion.Disable();
    }

    public void SwitchInputMode(InputMode newMode)
    {
        Disable();
        switch (newMode)
        {
            case InputMode.Game:
                inputActions.Locomotion.Enable();
                break;
            case InputMode.Menu:
                break;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        PlayerManager.instance.SetMoveInput(context.ReadValue<Vector2>());
    }

    private void OnCam(InputAction.CallbackContext context)
    {
        PlayerManager.instance.SetCamInput(context.ReadValue<Vector2>());
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        PlayerManager.instance.CallJump(context.performed);
    }
}
