using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    public int lives = 9;
    public float stamina = 2000f;
    public float drainRate = 500f;
    public float regenRate = 300f;
    public Vector2 startPos;
    public bool lights;

    public int silverKeys = 0;
    public int goldKeys = 0;

    // CHANGE: Use Sprite instead of Image for key icons
    [SerializeField] private Sprite silverKeySprite;
    [SerializeField] private Sprite goldKeySprite;

    private new Collider2D collider;

    void Start()
    {
        startPos = transform.position;
        collider = GetComponent<Collider2D>();
        lights = false;
    }

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

        UIManager.Instance.RefreshLivesUI();

        if (lives == 0)
        {
            Gameover();
        }
        Respawn();
    }

    void Respawn()
    {
        transform.position = startPos;
        //Time.timeScale = 0;
        //Task.Delay(1000);
        //Time.timeScale = 1f;
    }

    void Gameover()
    {

    }

    public void CollectSilverKey()
    {
        silverKeys++;
        UIManager.Instance.RefreshInventoryUI();
    }

    public void CollectGoldKey()
    {
        goldKeys++;
        UIManager.Instance.RefreshInventoryUI();
    }

    public bool HasSilverKey() => silverKeys > 0;
    public bool HasGoldKeys() => goldKeys >= 3;

    public void UseSilverKey()
    {
        if (HasSilverKey())
        {
            silverKeys--;
            UIManager.Instance.RefreshInventoryUI();
        }
    }

    public void UseGoldKeys()
    {
        if (HasGoldKeys())
        {
            goldKeys -= 3;
            UIManager.Instance.RefreshInventoryUI();
        }
    }

    // CHANGE: Return Sprites instead of Images
    public Sprite GetSilverKeySprite() => silverKeySprite;
    public Sprite GetGoldKeySprite() => goldKeySprite;
}