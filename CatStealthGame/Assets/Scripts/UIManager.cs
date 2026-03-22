using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Instance of the UIManager, used to allow other scripts to easily access the UIManager's functions without needing a reference to it
    public static UIManager Instance;

    // The text object used to show prompts to the player, such as when they can pick up a key
    public Text promptText;

    // The text object used to show temporary notifications to the player, such as when they take damage or collect a key
    [SerializeField] private Text messageText;

    // The image used to show the silver key in the player's inventory UI
    [SerializeField] private Image silverKeyImage;

    // The text used to show the number of silver keys the player has collected in the inventory UI
    [SerializeField] private Text silverKeyText;

    // The image used to show the gold key in the player's inventory UI
    [SerializeField] private Image goldKeyImage;

    // The text used to show the number of gold keys the player has collected in the inventory UI
    [SerializeField] private Text goldKeyText;

    // Reference to the player's resources, used to access the player's inventory and update the UI accordingly
    [SerializeField] private PlayerResources playerResources;

    // Destroys gameobject if another instance of the UIManager already exists, otherwise sets this instance as the singleton instance
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HidePrompt();
        messageText.gameObject.SetActive(false);

        // Initialize the key images to be invisible at the start of the game, since the player starts with 0 keys
        RefreshInventoryUI();
    }

    // Shows Prompt
    public void ShowPrompt(string message)
    {
        promptText.text = message;
        promptText.gameObject.SetActive(true);
    }

    // Hides Prompt
    public void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }

    // Shows a temporary message to the player, such as when they take damage or collect a key, then hides the message after a short delay
    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        StartCoroutine(HideMessageAfterDelay(2.5f));
    }

    // Coroutine to hide the temporary message after a delay, used by the ShowMessage function to automatically hide the message after a few seconds
    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
    }

    // Refreshes the inventory UI to show the current number of keys the player has collected, as well as updating the key images to show the correct sprites based on the player's inventory
    public void RefreshInventoryUI()
    {
        // Update silver key display
        if (playerResources == null) return;
        
        int silverCount = playerResources.silverKeys;
        int goldCount = playerResources.goldKeys;

        Sprite silverSprite = playerResources.GetSilverKeySprite();
        if (silverSprite != null)
            silverKeyImage.sprite = silverSprite;
        
        silverKeyText.text = silverCount == 1 ? "1 Silver Key" : silverCount + " Silver Keys";

        Sprite goldSprite = playerResources.GetGoldKeySprite();
        if (goldSprite != null)
            goldKeyImage.sprite = goldSprite;
        
        goldKeyText.text = goldCount == 1 ? "1 Gold Key" : goldCount + " Gold Keys";
    }
    
}
