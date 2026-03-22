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
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        RefreshInventoryUI();
        RefreshLivesUI(); // <-- ADD THIS
    }

    // Call this whenever keys change
    public void RefreshInventoryUI()
    {
        // Try to find player if not set
        if (playerResources == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                playerResources = player.GetComponent<PlayerResources>();
        }

        if (playerResources == null) return; // still not found

        // Silver keys
        Sprite silverSprite = playerResources.GetSilverKeySprite(); // <-- Sprite, not Image
        if (silverSprite != null)
            silverKeyImage.sprite = silverSprite;

        silverKeyText.text = playerResources.silverKeys.ToString();

        // Gold keys
        Sprite goldSprite = playerResources.GetGoldKeySprite(); // <-- Sprite, not Image
        if (goldSprite != null)
            goldKeyImage.sprite = goldSprite;

        goldKeyText.text = playerResources.goldKeys.ToString();
    }

    public void RefreshLivesUI()
    {
        if (playerResources == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                playerResources = player.GetComponent<PlayerResources>();
        }

        if (playerResources == null) return;

        livesText.text = "Lives: " + playerResources.lives.ToString();
    }
}