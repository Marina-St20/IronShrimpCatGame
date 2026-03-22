using UnityEngine;

// This script is attached to the cage objects in the game, and handles the logic for unlocking the cages when the player interacts with them, as well as showing the appropriate prompts to the player based on whether they have the necessary keys to unlock the cage or not
public class Cage : MonoBehaviour
{
    // Flag to track if the cage is locked or unlocked, used to decide if the player can open the cage or not
    [SerializeField] private bool isFinalCage = false;

    // The prefab for the open cage, used to visually indicate to the player that the cage has been unlocked
    [SerializeField] private GameObject OpenCagePrefab;

    // Flag to track if the cage has already been unlocked, to prevent the player from unlocking the same cage multiple times and potentially causing bugs with the inventory system
    private bool isUnocked = false;

    // Flag to track if the player is nearby, to decide wether the E key should work
    private bool playerNearby = false;

    // Reference to the player's resources, used to check if the player has the necessary keys to unlock the cage and to update the player's inventory when a cage is unlocked
    private PlayerResources playerResources;

    // Update is called once per frame
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !isUnocked)
        {
            OpenCage();
        }
    }

    // When the player enters the trigger area of the cage, set playerNearby to true and store a reference to the player's resources, then show the appropriate prompt based on whether the player has the necessary keys to unlock the cage or not
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
            playerResources = collision.GetComponentInParent<PlayerResources>();

            if(isUnocked) return;

            UpdatePrompt();
            // collision.GetComponentInParent<PlayerResources>().openCage();
            // // Temp Destroy
            // Destroy(gameObject);
        }
    }

    // When the player exits the trigger area of the cage, set playerNearby to false and clear the reference to the player's resources, then hide the prompt
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
            playerResources = null;
            UIManager.Instance.HidePrompt();
        }
    }

    // Update the prompt text based on whether the player has the necessary keys to unlock the cage or not, and whether this is the final cage or not
    private void UpdatePrompt()
    {
        if (playerResources == null) return;

        if (isFinalCage)
        {
            if (playerResources.HasGoldKeys())
                UIManager.Instance.ShowPrompt("[E] Unlock Final Cage");
            else
                UIManager.Instance.ShowPrompt("[E] Unlock Final Cage - Needs 3 Gold Keys");
        }
        else
        {
            if (playerResources.HasSilverKey())
                UIManager.Instance.ShowPrompt("[E] Unlock Cage");
            else
                UIManager.Instance.ShowPrompt("[E] Unlock Cage - Needs 1 Silver Key");
        }
    }

    // Check if the player has the necessary keys to unlock the cage, and if so, unlock the cage and update the player's inventory accordingly, then show a message to the player indicating that the cage has been unlocked
    private void OpenCage()
    {
        if (playerResources == null) return;

        bool canUnlock = isFinalCage
            ? playerResources.HasGoldKeys()
            : playerResources.HasSilverKey();
        
        if (!canUnlock)
        {
            UpdatePrompt();
            return;
        }

        isUnocked = true;

        if (isFinalCage)
            playerResources.UseGoldKeys();
        else
            playerResources.UseSilverKey();

        if (OpenCagePrefab != null)
        {
            Instantiate(OpenCagePrefab, transform.position, transform.rotation);
        }
        
        UIManager.Instance.HidePrompt();
        UIManager.Instance.ShowMessage(isFinalCage ? "Final Cage Unlocked! You freed your family!" : "Cage Unlocked! You freed trapped cats!");

        // TODO: Add some kind of animation or effect to show the cage opening, and then destroy the cage object after a short delay to allow the player to see the effect
        // GameProgressionManager.Instance.OnCageUnlocked(isFinalCage);
    }
}
