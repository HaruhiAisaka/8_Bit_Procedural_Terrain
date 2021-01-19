using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private Lerp lerp;

    [Header("Movement Options")]
    public float durationOfMovementPerTile = 0.5f;
    private Vector2 movementDirection;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lerp = new Lerp();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public void UpdateMovementDirection(Vector2 vector)
    {
        // Makes sure that the movement direction is only updated if it is a
        // vector that goes in the four cardinal directions.
        if (vector.x == 0 || vector.y == 0)
        {
            movementDirection = vector;
        }
    }

    private void MovePlayer()
    {
        if (!lerp.transformLerpRunning)
        {
            UpdateAnimator();
            if (!movementDirection.Equals(Vector2.zero))
            {
                Vector3 nextPosition = 
                    this.transform.position + (Vector3)(movementDirection * Tile.tileSize);
                StartCoroutine(
                    lerp.TransformOverTime(
                        this.transform, 
                        nextPosition, 
                        durationOfMovementPerTile));
            }
        }
    }
    
    private void UpdateAnimator()
    {
        if (movementDirection.Equals(Vector2.zero))
        {
            animator.speed = 0;
        } 
        else
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
            animator.speed = 1;
        }
    }
}
