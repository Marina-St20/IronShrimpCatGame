using UnityEngine;

public class PatrolFlashlight : MonoBehaviour
{
    [SerializeField] public float viewDistance = 5f;
    [SerializeField] public float viewAngle = 90f; // full cone angle
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float beamRadius = 0.1f;
    [SerializeField] private bool debugLogs = true;

    private Transform playerTransform;
    private PlayerResources playerResources;

    private void Start()
    {
        FindPlayer();

        if (obstacleMask == 0)
        {
            obstacleMask = LayerMask.GetMask("Movable", "Wall", "Walls");
        }
    }

    private void Update()
    {
        if (playerTransform == null || playerResources == null)
        {
            FindPlayer();
            if (playerTransform == null || playerResources == null) return;
        }

        Vector2 origin = transform.position;
        Vector2 toPlayer = (Vector2)playerTransform.position - origin;

        if (toPlayer.sqrMagnitude > viewDistance * viewDistance)
            return;

        float halfAngle = viewAngle * 0.5f;
        float angleToPlayer = Vector2.Angle(transform.up, toPlayer.normalized);

        if (angleToPlayer > halfAngle)
            return;

        RaycastHit2D hit = Physics2D.CircleCast(origin, beamRadius, toPlayer.normalized, toPlayer.magnitude, obstacleMask);

        if (hit.collider != null)
        {
            if (debugLogs)
                Debug.Log("Beam blocked by: " + hit.collider.name);

            return;
        }

        if (debugLogs)
            Debug.Log("Player hit by patrol beam");

        playerResources.Die();
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null) return;

        playerTransform = playerObj.transform;

        playerResources =
            playerObj.GetComponent<PlayerResources>() ??
            playerObj.GetComponentInParent<PlayerResources>() ??
            playerObj.GetComponentInChildren<PlayerResources>();

        if (debugLogs && playerResources == null)
        {
            Debug.LogWarning("Player found, but PlayerResources was not found on it, parent, or children.");
        }
    }
}