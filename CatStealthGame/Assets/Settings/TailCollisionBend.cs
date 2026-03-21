using UnityEngine;

public class TailCollisionBend : MonoBehaviour
{
    public Transform visualRoot;

    public Transform tailBone1;
    public Transform tailBone2;
    public Transform tailBone3;

    public Transform tailBaseMarker;
    public Transform tailMidMarker;
    public Transform tailTipMarker;

    public LayerMask wallMask;

    public float baseSensorRadius = 0.24f;
    public float midSensorRadius = 0.28f;
    public float tipSensorRadius = 0.34f;

    public float tuckBone1 = 18f;
    public float tuckBone2 = 40f;
    public float tuckBone3 = 65f;

    public float collisionTurnSpeed = 1200f;
    public float recoveryTurnSpeed = 320f;

    public float idleSpeed = 1.8f;
    public float idleBone1 = 2f;
    public float idleBone2 = 4f;
    public float idleBone3 = 7f;

    public float wallStrengthThreshold = 0.10f;
    public float wallBlendInSpeed = 6f;
    public float wallBlendOutSpeed = 4f;

    public bool invertBend = false;

    private float baseZ1;
    private float baseZ2;
    private float baseZ3;

    private float currentOffset1;
    private float currentOffset2;
    private float currentOffset3;

    private float lastSide = 1f;
    private float wallBlend = 0f;

    void Start()
    {
        baseZ1 = tailBone1.localEulerAngles.z;
        baseZ2 = tailBone2.localEulerAngles.z;
        baseZ3 = tailBone3.localEulerAngles.z;
    }

    void LateUpdate()
    {
        float dt = Mathf.Max(Time.deltaTime, 0.0001f);

        float side;
        float wallStrength;
        bool nearWall = TryGetWallInfo(out side, out wallStrength);

        if (nearWall)
        {
            if (invertBend) side *= -1f;
            if (Mathf.Abs(side) < 0.01f) side = lastSide;
            lastSide = side;

            wallBlend = Mathf.MoveTowards(wallBlend, wallStrength, wallBlendInSpeed * dt);
        }
        else
        {
            wallBlend = Mathf.MoveTowards(wallBlend, 0f, wallBlendOutSpeed * dt);
            side = lastSide;
        }

        float t = Time.time * idleSpeed;

        float idleTarget1 = Mathf.Sin(t + 0.0f) * idleBone1;
        float idleTarget2 = Mathf.Sin(t + 0.6f) * idleBone2;
        float idleTarget3 = Mathf.Sin(t + 1.2f) * idleBone3;

        float tuckTarget1 = side * tuckBone1;
        float tuckTarget2 = side * tuckBone2;
        float tuckTarget3 = side * tuckBone3;

        float target1 = Mathf.Lerp(idleTarget1, tuckTarget1, wallBlend);
        float target2 = Mathf.Lerp(idleTarget2, tuckTarget2, wallBlend);
        float target3 = Mathf.Lerp(idleTarget3, tuckTarget3, wallBlend);

        float speed = wallBlend > 0.01f ? collisionTurnSpeed : recoveryTurnSpeed;

        currentOffset1 = Mathf.MoveTowardsAngle(currentOffset1, target1, speed * dt);
        currentOffset2 = Mathf.MoveTowardsAngle(currentOffset2, target2, speed * dt);
        currentOffset3 = Mathf.MoveTowardsAngle(currentOffset3, target3, speed * dt);

        tailBone1.localRotation = Quaternion.Euler(0f, 0f, baseZ1 + currentOffset1);
        tailBone2.localRotation = Quaternion.Euler(0f, 0f, baseZ2 + currentOffset2);
        tailBone3.localRotation = Quaternion.Euler(0f, 0f, baseZ3 + currentOffset3);
    }

    bool TryGetWallInfo(out float side, out float strength)
    {
        side = 0f;
        strength = 0f;

        float s1, str1;
        bool h1 = MarkerHitsWall(tailBaseMarker, baseSensorRadius, out s1, out str1);

        float s2, str2;
        bool h2 = MarkerHitsWall(tailMidMarker, midSensorRadius, out s2, out str2);

        float s3, str3;
        bool h3 = MarkerHitsWall(tailTipMarker, tipSensorRadius, out s3, out str3);

        float bestStrength = 0f;
        float bestSide = 0f;

        if (h1 && str1 > bestStrength)
        {
            bestStrength = str1;
            bestSide = s1;
        }

        if (h2 && str2 > bestStrength)
        {
            bestStrength = str2;
            bestSide = s2;
        }

        if (h3 && str3 > bestStrength)
        {
            bestStrength = str3;
            bestSide = s3;
        }

        if (bestStrength < wallStrengthThreshold)
            return false;

        side = Mathf.Abs(bestSide) < 0.01f ? lastSide : bestSide;
        strength = bestStrength;

        return true;
    }

    bool MarkerHitsWall(Transform marker, float radius, out float side, out float strength)
    {
        side = 0f;
        strength = 0f;

        if (marker == null) return false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(marker.position, radius, wallMask);
        if (hits == null || hits.Length == 0) return false;

        bool found = false;
        float bestStrength = 0f;

        foreach (Collider2D hit in hits)
        {
            Vector2 p = marker.position;
            Vector2 closest = hit.ClosestPoint(p);
            Vector2 away = p - closest;

            float dist = away.magnitude;
            Vector2 awayDir;

            if (dist < 0.0001f)
            {
                awayDir = p - (Vector2)hit.bounds.center;
                if (awayDir.sqrMagnitude < 0.0001f)
                    awayDir = Vector2.right;
                awayDir.Normalize();
                dist = 0f;
            }
            else
            {
                awayDir = away / dist;
            }

            Vector2 localAway = visualRoot.InverseTransformDirection(awayDir);
            float x = localAway.x;

            float candidateSide = Mathf.Abs(x) < 0.05f ? lastSide : Mathf.Sign(x);
            float candidateStrength = Mathf.Clamp01((radius - dist) / radius);

            if (candidateStrength > bestStrength)
            {
                bestStrength = candidateStrength;
                side = candidateSide;
                strength = candidateStrength;
                found = true;
            }
        }

        return found;
    }
}