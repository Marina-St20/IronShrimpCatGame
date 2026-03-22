using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool canMove = true;

    // Drag your VisualRoot here
    public Transform visualRoot;

    private Rigidbody2D rb;
    private Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove)
        {
            input = Vector2.zero;
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        if (visualRoot == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 forward = visualRoot.up;
        Vector2 right = visualRoot.right;

        Vector2 moveDirection = (right * input.x + forward * input.y).normalized;

        rb.linearVelocity = moveDirection * moveSpeed;
    }
}