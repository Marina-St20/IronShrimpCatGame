using UnityEngine;

public class CatAimController : MonoBehaviour
{
    public Transform visualRoot;
    public Transform headPivot;
    public Transform tailPivot;

    public float maxHeadTurn = 20f;
    public float bodySmoothTime = 0.08f;
    public float tailSmoothTime = 0.18f;
    public float maxTailTurn = 25f;

    private float bodyAngularVelocity;
    private float tailAngularVelocity;

    void Update()
    {
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        Vector2 mousePos2D = new Vector2(mouseWorld.x, mouseWorld.y);
        Vector2 dir = mousePos2D - (Vector2)transform.position;

        if (dir.sqrMagnitude < 0.0001f)
            return;

        // sprite faces UP by default
        float rawAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        // body target snaps to 8 directions
        float snappedAngle = Mathf.Round(rawAngle / 45f) * 45f;

        // smooth body rotation
        float currentBodyAngle = visualRoot.eulerAngles.z;
        float smoothBodyAngle = Mathf.SmoothDampAngle(
            currentBodyAngle,
            snappedAngle,
            ref bodyAngularVelocity,
            bodySmoothTime
        );

        visualRoot.rotation = Quaternion.Euler(0f, 0f, smoothBodyAngle);

        // head leads a little
        float headOffset = Mathf.DeltaAngle(smoothBodyAngle, rawAngle);
        headOffset = Mathf.Clamp(headOffset, -maxHeadTurn, maxHeadTurn);
        headPivot.localRotation = Quaternion.Euler(0f, 0f, headOffset);

        // tail follows behind
        float tailTarget = -headOffset;
        tailTarget = Mathf.Clamp(tailTarget, -maxTailTurn, maxTailTurn);

        float currentTailAngle = tailPivot.localEulerAngles.z;
        float smoothTailAngle = Mathf.SmoothDampAngle(
            currentTailAngle,
            tailTarget,
            ref tailAngularVelocity,
            tailSmoothTime
        );

        tailPivot.localRotation = Quaternion.Euler(0f, 0f, smoothTailAngle);
    }
}