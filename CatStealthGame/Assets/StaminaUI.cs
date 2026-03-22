using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{

    public PlayerResources player;
    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.stamina / 2000f;
    }
}
