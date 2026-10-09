using System;
using System.Diagnostics;
using InGame.Input;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static readonly Subject<Unit> _next = new();

    public static Observable<Unit> Next => _next;

    private static GameInput _gameInput;
    private void Start()
    {
        _gameInput = new();

        SubscribeCallback(_gameInput.Player.Next,OnNext);

        _gameInput.Enable();
    }

    private void SubscribeCallback(InputAction action ,Action<InputAction.CallbackContext> callback)
    {
        action.performed += callback;
        action.canceled += callback;
    }

    private void OnNext(InputAction.CallbackContext callback)
    {
        if (callback.ReadValueAsButton())
            _next.OnNext(Unit.Default);
    }
}
