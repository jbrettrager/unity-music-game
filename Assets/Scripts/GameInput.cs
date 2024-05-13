using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnNoteLeftAction;
    public event EventHandler OnNoteRightAction;
    public event EventHandler OnNoteEditAction;
    private PlayerInputActions playerInputActions;
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.NoteLeft.started += NoteLeft_performed;
        playerInputActions.Player.NoteRight.started += NoteRight_performed;
        playerInputActions.Player.NoteEdit.started += NoteEdit_performed;
    }

    private void NoteLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnNoteLeftAction?.Invoke(this, EventArgs.Empty);
    }
    private void NoteRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnNoteRightAction?.Invoke(this, EventArgs.Empty);
    }

    private void NoteEdit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnNoteEditAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.x = +1;
        inputVector = inputVector.normalized;

        return inputVector;

    }

}
