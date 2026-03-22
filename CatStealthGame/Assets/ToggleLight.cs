using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.EventSystems.EventTrigger;

public class ToggleLight : MonoBehaviour
{

    public Light2D leftflashlight;

    public Light2D rightflashlight;

    public PlayerResources playerResources;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!playerResources.lights && playerResources.stamina <= 0)
                return;

            playerResources.lights = !playerResources.lights;
        }

        // Apply light state
        leftflashlight.enabled = playerResources.lights;
        rightflashlight.enabled = playerResources.lights;
    }
}
