using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Inventory UI")]
    [SerializeField] private Image silverKeyImage;
    [SerializeField] private TextMeshProUGUI silverKeyText;
    [SerializeField] private Image goldKeyImage;
    [SerializeField] private TextMeshProUGUI goldKeyText;
    [SerializeField] private TextMeshProUGUI livesText;

    private PlayerResources playerResources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindPlayerResources();
        RefreshInventoryUI();
        RefreshLivesUI();
    }

    private void FindPlayerResources()
    {
        if (playerResources != null) return;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerResources = player.GetComponent<PlayerResources>();
        }
    }

    public void SetPlayerResources(PlayerResources resources)
    {
        playerResources = resources;
    }

    public void RefreshInventoryUI()
    {
        FindPlayerResources();

        if (playerResources == null) return;

        if (silverKeyImage != null)
        {
            Sprite silverSprite = playerResources.GetSilverKeySprite();
            if (silverSprite != null)
            {
                silverKeyImage.sprite = silverSprite;
                silverKeyImage.enabled = true;
            }
        }

        if (silverKeyText != null)
        {
            silverKeyText.text = playerResources.silverKeys.ToString();
        }

        if (goldKeyImage != null)
        {
            Sprite goldSprite = playerResources.GetGoldKeySprite();
            if (goldSprite != null)
            {
                goldKeyImage.sprite = goldSprite;
                goldKeyImage.enabled = true;
            }
        }

        if (goldKeyText != null)
        {
            goldKeyText.text = playerResources.goldKeys.ToString();
        }
    }

    public void RefreshLivesUI()
    {
        FindPlayerResources();

        if (playerResources == null) return;

        if (livesText != null)
        {
            livesText.text = "Lives: " + playerResources.lives.ToString();
        }
    }
}