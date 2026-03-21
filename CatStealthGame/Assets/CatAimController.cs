using UnityEngine;

public class CatAimController : MonoBehaviour
{
    public Transform visualRoot;
    public Transform headPivot;

    public float maxHeadTurn = 25f;
    public float bodyTurnSpeed = 1000f;

    void Update()
    {
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        Vector2 dir = mouseWorld - transform.position;

        if (dir.sqrMagnitude < 0.0001f)
            return;

        float rawAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        float snappedAngle = Mathf.Round(rawAngle / 45f) * 45f;

        float currentBodyAngle = visualRoot.eulerAngles.z;
        float smoothBodyAngle = Mathf.MoveTowardsAngle(
            currentBodyAngle,
            snappedAngle,
            bodyTurnSpeed * Time.deltaTime
        );

        visualRoot.rotation = Quaternion.Euler(0f, 0f, smoothBodyAngle);

        float headOffset = Mathf.DeltaAngle(smoothBodyAngle, rawAngle);
        headOffset = Mathf.Clamp(headOffset, -maxHeadTurn, maxHeadTurn);

        headPivot.localRotation = Quaternion.Euler(0f, 0f, headOffset);
    }
}