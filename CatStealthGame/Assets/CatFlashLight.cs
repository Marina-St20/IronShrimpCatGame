using UnityEngine;

public class CatFlashLight : MonoBehaviour
{
    public Transform head;
    public Transform leftEye;
    public Transform rightEye;

    // how far the light should start in front of the eyes
    public float forwardOffset = 0.03f;

    // use this if the light cone sprite/light points the wrong way
    public float angleOffset = 0f;

    void LateUpdate()
    {
        if (head == null || leftEye == null || rightEye == null)
            return;

        // midpoint between both eyes
        Vector3 eyeMid = (leftEye.position + rightEye.position) * 0.5f;

        // your cat/head faces UP by default in your aiming setup
        Vector3 origin = eyeMid + head.up * forwardOffset;

        transform.position = origin;
        transform.rotation = head.rotation * Quaternion.Euler(0f, 0f, angleOffset);
    }
}