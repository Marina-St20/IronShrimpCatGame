using UnityEngine;

public class TailController : MonoBehaviour
{
    public Transform visualRoot;      // your body/root visual rotation
    public Transform tailPivot;       // pivot at base of tail
    public Transform tailTipMarker;   // empty object at tail tip
    public LayerMask wallMask;

    public float lagStrength = 0.02f;
    public float maxLagAngle = 18f;
    public float smoothTime = 0.08f;

    public float sensorRadius = 0.08f;
    public float maxCollisionBend = 30f;
    public float collisionInfluence = 1f;

    private float tailAngularVelocity;
    private float lastBodyAngle;

    void Start()
    {
        lastBodyAngle = visualRoot.eulerAngles.z;
    }

    void LateUpdate()
    {
        float bodyAngle = visualRoot.eulerAngles.z;

        float bodyAngularSpeed =
            Mathf.DeltaAngle(lastBodyAngle, bodyAngle) / Mathf.Max(Time.deltaTime, 0.0001f);

        lastBodyAngle = bodyAngle;

        // Tail lags behind turning
        float lagOffset = Mathf.Clamp(
            -bodyAngularSpeed * lagStrength,
            -maxLagAngle,
            maxLagAngle
        );

        // Tail bends away from wall if tail tip is close
        float collisionOffset = 0f;

        Collider2D hit = Physics2D.OverlapCircle(tailTipMarker.position, sensorRadius, wallMask);
        if (hit != null)
        {
            Vector2 tip = tailTipMarker.position;
            Vector2 closest = hit.ClosestPoint(tip);
            Vector2 away = tip - closest;

            if (away.sqrMagnitude < 0.0001f)
                away = -(Vector2)visualRoot.up;

            float awayAngle = Mathf.Atan2(away.y, away.x) * Mathf.Rad2Deg - 90f;

            collisionOffset = Mathf.DeltaAngle(bodyAngle, awayAngle);
            collisionOffset = Mathf.Clamp(collisionOffset, -maxCollisionBend, maxCollisionBend);
            collisionOffset *= collisionInfluence;
        }

        float targetLocalAngle = Mathf.Clamp(
            lagOffset + collisionOffset,
            -maxCollisionBend,
            maxCollisionBend
        );

        float smoothLocalAngle = Mathf.SmoothDampAngle(
            tailPivot.localEulerAngles.z,
            targetLocalAngle,
            ref tailAngularVelocity,
            smoothTime
        );

        tailPivot.localRotation = Quaternion.Euler(0f, 0f, smoothLocalAngle);
    }

    void OnDrawGizmosSelected()
    {
        if (tailTipMarker == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(tailTipMarker.position, sensorRadius);
    }
}