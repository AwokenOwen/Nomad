using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerActions inputActions;

    InputAction movementAction;

    private void Awake()
    {
        inputActions = new PlayerActions();
        movementAction = inputActions.Locomotion.Movement;

        movementAction.performed += OnMove;
        movementAction.canceled += OnMove;
    }

    private void OnEnable()
    {
        inputActions.Locomotion.Enable();
    }

    private void OnDisable()
    {
        movementAction.performed -= OnMove;
        movementAction.canceled -= OnMove;

        inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        PlayerManager.instance.SetMoveVec(context.ReadValue<Vector2>());
    }
}
