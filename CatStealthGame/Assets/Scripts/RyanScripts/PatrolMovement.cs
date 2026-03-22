using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    [SerializeField] private List<Vector2> Points = new List<Vector2>();
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private bool Loop = false;

    [Header("Obstacle detection")]
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float obstacleCheckDistance = 0.2f;
    [SerializeField] private Transform obstacleCheck;

    private int CurrentPoint = 0;
    private int Direction = 1;

    private void Start()
    {
        if (obstacleCheck == null)
        {
            obstacleCheck = transform.Find("ObstacleCheck");
        }

        if (obstacleMask == 0)
        {
            obstacleMask = LayerMask.GetMask("Movable", "Wall", "Walls");
        }

        if (Points.Count == 0) return;
        transform.position = Points[0];
    }

    private void Update()
    {
        if (Points.Count == 0) return;

        Vector2 pointToGoTo = Points[CurrentPoint];
        Vector2 currentPos = transform.position;

        if (Vector2.Distance(currentPos, pointToGoTo) < 0.01f)
        {
            UpdateCurrentPoint();
            pointToGoTo = Points[CurrentPoint];
        }

        Vector2 moveDir = (pointToGoTo - (Vector2)transform.position).normalized;

        if (moveDir.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if (IsObstacleAhead(moveDir))
        {
            ReverseDirection();
            return;
        }

        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPos, pointToGoTo, step);
    }

    private bool IsObstacleAhead(Vector2 moveDir)
    {
        if (moveDir.sqrMagnitude < 0.0001f) return false;

        Vector2 origin = obstacleCheck != null ? obstacleCheck.position : transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, moveDir, obstacleCheckDistance, obstacleMask);

        return hit.collider != null;
    }

    private void ReverseDirection()
    {
        if (Points.Count <= 1) return;

        Direction *= -1;

        if (Loop)
        {
            CurrentPoint = (CurrentPoint + Direction + Points.Count) % Points.Count;
        }
        else
        {
            CurrentPoint += Direction;
            CurrentPoint = Mathf.Clamp(CurrentPoint, 0, Points.Count - 1);
        }
    }

    private void UpdateCurrentPoint()
    {
        if (Loop)
        {
            CurrentPoint = (CurrentPoint + 1) % Points.Count;
        }
        else
        {
            CurrentPoint += Direction;

            if (CurrentPoint >= Points.Count)
            {
                CurrentPoint = Points.Count - 2;
                Direction = -1;
            }
            else if (CurrentPoint < 0)
            {
                CurrentPoint = 1;
                Direction = 1;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (obstacleCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(obstacleCheck.position, 0.05f);
        Gizmos.DrawLine(obstacleCheck.position, obstacleCheck.position + transform.up * obstacleCheckDistance);
    }
}