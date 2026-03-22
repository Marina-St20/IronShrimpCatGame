using UnityEngine;

public class OldPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool canMove = true;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove)
            return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}