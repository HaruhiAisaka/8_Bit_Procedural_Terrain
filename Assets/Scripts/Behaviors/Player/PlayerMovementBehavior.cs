using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Vector2 movementDirection;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Debug.Log("movementDirection: " + movementDirection);
        MovePlayer();
    }

    public void UpdateMovementDirection(Vector2 vector)
    {
        movementDirection = vector;
    }

    private void MovePlayer()
    {
        playerRigidbody.velocity = movementDirection;
    }
}
