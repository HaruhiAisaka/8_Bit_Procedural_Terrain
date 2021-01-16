using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    [Header("Components")]
    public PlayerMovementBehavior playerMovementBehavior;

    PlayerControls controls;
    Vector2 move;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Movement.performed += ctx => 
            SendMessage(ctx.ReadValue<Vector2>());
    }

    void SendMessage(Vector2 value)
    {
        playerMovementBehavior.UpdateMovementDirection(value);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

}
