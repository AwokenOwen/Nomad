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
    static PlayerActions inputActions;

    //Locomotion inputs
    InputAction movementAction;
    InputAction cameraAction;
    InputAction jumpAction;
    InputAction escapeAction;
    InputAction openGrimoreAction;

    //Menu inputs
    InputAction menuNavigationAction;
    InputAction menuSubmitAction;
    InputAction menuEscapeAction;
    InputAction menuCloseAction;
    InputAction menuTabNavAction;

    private void Awake()
    {
        GameManager.ChangeInputEvent += SwitchInputMode;

        inputActions = new PlayerActions();

        //Locomotion inputs
        movementAction = inputActions.Locomotion.Movement;
        cameraAction = inputActions.Locomotion.Camera;
        jumpAction = inputActions.Locomotion.Jump;
        escapeAction = inputActions.Locomotion.Escape;
        openGrimoreAction = inputActions.Locomotion.OpenGrimore;

        movementAction.performed += OnMove;
        movementAction.canceled += OnMove;
        cameraAction.performed += OnCam;
        cameraAction.canceled += OnCam;
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJump;
        escapeAction.performed += OnEscapeLocomotion;
        openGrimoreAction.performed += OnOpenGrimore;


        //Menu inputs
        menuNavigationAction = inputActions.Menu.Navigation;
        menuSubmitAction = inputActions.Menu.Select;
        menuEscapeAction = inputActions.Menu.Back;
        menuCloseAction = inputActions.Menu.CloseMenu;
        menuTabNavAction = inputActions.Menu.TabNav;

        menuNavigationAction.performed += OnNavigation;
        menuSubmitAction.performed += OnSubmit;
        menuEscapeAction.performed += OnEscapeMenu;
        menuCloseAction.performed += OnCloseMenu;
        menuTabNavAction.performed += OnTabNav;
    }

    private void OnEnable()
    {
        inputActions.Menu.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        escapeAction.performed -= OnEscapeLocomotion;
        openGrimoreAction.performed -= OnOpenGrimore;

        //Menu inputs
        menuNavigationAction.performed -= OnNavigation;
        menuSubmitAction.performed -= OnSubmit;
        menuEscapeAction.performed -= OnEscapeMenu;
        menuCloseAction.performed -= OnCloseMenu;
        menuTabNavAction.performed-= OnTabNav;

        Disable();
    }

    private static void Disable()
    {
        inputActions.Locomotion.Disable();
        inputActions.Menu.Disable();
    }

    public static void SwitchInputMode(InputMode newMode)
    {
        Disable();
        switch (newMode)
        {
            case InputMode.Game:
                inputActions.Locomotion.Enable();
                break;
            case InputMode.Menu:
                inputActions.Menu.Enable();
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

    private void OnEscapeLocomotion(InputAction.CallbackContext context)
    {
        SwitchInputMode(InputMode.Menu);
        PlayerManager.instance.OpenPauseMenu();
    }

    private void OnOpenGrimore(InputAction.CallbackContext context)
    {
        SwitchInputMode(InputMode.Menu);
        PlayerManager.instance.OpenGrimore();
    }

    private void OnEscapeMenu(InputAction.CallbackContext context)
    {
        GameManager.instance.MenuBack();
    }
    
    private void OnNavigation(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        GameManager.instance.MenuNavigate(input);
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        GameManager.instance.MenuSubmit();
    }

    private void OnCloseMenu(InputAction.CallbackContext context)
    {
        GameManager.instance.MenuClose();
    }

    private void OnTabNav(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        if (value != 0f)
            GameManager.instance.MenuTabNav(value);
    }
}
