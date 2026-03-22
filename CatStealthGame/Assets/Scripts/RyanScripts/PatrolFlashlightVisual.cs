using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PatrolFlashlightVisual : MonoBehaviour
{
    [SerializeField] private Light2D light2D;
    void Awake()
    {
        if(light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PatrolFlashlight patrolFlashlight = GetComponent<PatrolFlashlight>();

        light2D.pointLightInnerRadius = 0;
        light2D.pointLightOuterRadius = patrolFlashlight.viewDistance;
        light2D.pointLightInnerAngle = patrolFlashlight.viewAngle;
        light2D.pointLightOuterAngle = patrolFlashlight.viewAngle;
    }
}
