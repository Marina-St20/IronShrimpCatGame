using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PatrolFlashlightVisual : MonoBehaviour
{
    [SerializeField] private Light2D light2D;

    private void Awake()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }
    }

    private void Start()
    {
        PatrolFlashlight patrolFlashlight = GetComponent<PatrolFlashlight>();

        if (light2D == null || patrolFlashlight == null) return;

        light2D.pointLightInnerRadius = 0f;
        light2D.pointLightOuterRadius = patrolFlashlight.viewDistance;
        light2D.pointLightInnerAngle = patrolFlashlight.viewAngle;
        light2D.pointLightOuterAngle = patrolFlashlight.viewAngle;
    }
}