using UnityEngine;
using UnityEngine.Playables;

public class Die : MonoBehaviour
{
    int lives;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void die()
    {
        lives -= 1;
        respawn();
    }

    void respawn()
    {
        
    }
}
