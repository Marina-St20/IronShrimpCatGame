using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public int lives = 9;
    public int stamina = 2000;
    public int keys = 0;
    public Vector2 startPos;
    public bool lights;

    private new Collider2D collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        collider = GetComponent<Collider2D>();
        lights = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lights)
        {
            stamina = Math.Max(0, stamina - 1);
        }
        else
        {
            stamina = Math.Min(2000, stamina + 1);
        }
    }

    public void die()
    {
        lives -= 1;
        if (lives == 0)
        {
            gameover();
        }
        respawn();

    }

    void respawn()
    {
        transform.position = startPos;
        // Not working ig
        //Time.timeScale = 0;
        //Task.Delay(1000);
        //Time.timeScale = 1f;
    }

    void gameover()
    {

    }

    public void openCage()
    {
        // Cage.open();
        keys--;
    }

    public void collectKey()
    {
        keys++;
    }
}
