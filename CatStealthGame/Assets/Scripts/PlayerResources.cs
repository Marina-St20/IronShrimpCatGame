using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public int lives = 9;
    public float stamina = 2000f;
    public float drainRate = 500f;
    public float regenRate = 300f;
    //public int keys = 0;
    public Vector2 startPos;
    public bool lights;

    // The number of silver keys the player has collected
    public int silverKeys = 0;
    // The number of gold keys the player has collected
    public int goldKeys = 0;

    // The sprites for the silver keys, used in the UI to show the player's inventory
    [SerializeField] private Sprite silverKeySprite;
    // The sprites for the gold keys, used in the UI to show the player's inventory
    [SerializeField] private Sprite goldKeySprite;

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
            stamina -= drainRate * Time.deltaTime;

            if (stamina <= 0)
            {
                stamina = 0;
                lights = false;
            }
        }
        else
        {
            stamina += regenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, 2000f);
        }
    }

    public void Die()
    {
        lives -= 1;
        if (lives == 0)
        {
            Gameover();
        }
        Respawn();

    }

    void Respawn()
    {
        transform.position = startPos;
        // Not working ig
        //Time.timeScale = 0;
        //Task.Delay(1000);
        //Time.timeScale = 1f;
    }

    void Gameover()
    {

    }

    // public void openCage()
    // {
    //     // Cage.open();
    //     keys--;
    // }

    // public void collectKey()
    // {
    //     keys++;
    // }

    // Updates the player's silver key count and refreshes the inventory UI to reflect the change
    public void CollectSilverKey()
    {
        silverKeys++;

        UIManager.Instance.RefreshInventoryUI();
    }

    // Updates the player's gold key count and refreshes the inventory UI to reflect the change
    public void CollectGoldKey()
    {
        goldKeys++;
        UIManager.Instance.RefreshInventoryUI();
    }

    // Checks if the player has at least one silver key
    public bool HasSilverKey() => silverKeys > 0;

    // Checks if the player has at least three gold keys
    public bool HasGoldKeys() => goldKeys >= 3;

    // Uses one silver key if the player has any, and refreshes the inventory UI
    public void UseSilverKey()
    {
        if (HasSilverKey())
        {
            silverKeys--;
            UIManager.Instance.RefreshInventoryUI();
        }
    }

    // Uses three gold keys if the player has at least three, and refreshes the inventory UI
    public void UseGoldKeys()
    {
        if (HasGoldKeys())
        {
            goldKeys -= 3;
            UIManager.Instance.RefreshInventoryUI();
        }
    }

    // Returns the sprite for the silver key, used in the UI to show the player's inventory
    public Sprite GetSilverKeySprite() => silverKeySprite;

    // Returns the sprite for the gold key, used in the UI to show the player's inventory
    public Sprite GetGoldKeySprite() => goldKeySprite;
}
