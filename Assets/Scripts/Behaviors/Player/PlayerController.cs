using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Sub Behaviours")]
    public PlayerMovementBehavior playerMovementBehavior;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    private Vector3 rawInputMovement;
    
    // Action Maps
    private string actionMapPlayerControls = "Player Controls";
    private string actionMapMenuControls = "Menu Controls";

    // Current Control Scheme
    private string currentControlScheme;

    public void SetupPlayer()
    {
        currentControlScheme = playerInput.currentControlScheme;

        playerMovementBehavior.SetupBehavior();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    void Update()
    {
        UpdatePlayerMovement();
    }

    void UpdatePlayerMovement()
    {
        playerMovementBehavior.UpdateMovementData(rawInputMovement);
    }

}
