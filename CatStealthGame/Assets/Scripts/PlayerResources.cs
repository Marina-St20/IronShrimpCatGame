using UnityEngine;

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

    [SerializeField] private Sprite silverKeySprite;
    [SerializeField] private Sprite goldKeySprite;

    private new Collider2D collider;

    void Start()
    {
        startPos = transform.position;
        collider = GetComponent<Collider2D>();
        lights = false;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.SetPlayerResources(this);
            UIManager.Instance.RefreshInventoryUI();
            UIManager.Instance.RefreshLivesUI();
        }
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

        if (UIManager.Instance != null)
            UIManager.Instance.RefreshLivesUI();

        if (lives <= 0)
        {
            lives = 0;
            Gameover();
        }
        else
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startPos;
    }

    void Gameover()
    {
        Debug.Log("Game Over");
    }

    public void CollectSilverKey()
    {
        silverKeys++;

        if (UIManager.Instance != null)
            UIManager.Instance.RefreshInventoryUI();
    }

    public void CollectGoldKey()
    {
        goldKeys++;

        if (UIManager.Instance != null)
            UIManager.Instance.RefreshInventoryUI();
    }

    public bool HasSilverKey()
    {
        return silverKeys > 0;
    }

    public bool HasGoldKeys()
    {
        return goldKeys >= 3;
    }

    public void UseSilverKey()
    {
        if (HasSilverKey())
        {
            silverKeys--;

            if (UIManager.Instance != null)
                UIManager.Instance.RefreshInventoryUI();
        }
    }

    public void UseGoldKeys()
    {
        if (HasGoldKeys())
        {
            goldKeys -= 3;

            if (UIManager.Instance != null)
                UIManager.Instance.RefreshInventoryUI();
        }
    }

    public Sprite GetSilverKeySprite()
    {
        return silverKeySprite;
    }

    public Sprite GetGoldKeySprite()
    {
        return goldKeySprite;
    }
}