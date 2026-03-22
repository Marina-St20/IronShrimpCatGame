using UnityEngine;

// Different types of keys that can be collected by the player
public enum KeyType
{
    Silver,
    Gold,
}

public class Key : MonoBehaviour
{
    // The type of key this object represents
    [SerializeField] private KeyType keyType; 

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // Flag to track if the player is nearby, to decide wether the E key should work
    private bool playerNearby = false;

    // Reference to the player's resources, used to update the player's inventory when a key is collected
    private PlayerResources playerResources;

    // Update is called once per frame
    void Update()
    {
        // Only listen for E when the player is actually nearby
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickupKey();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;

            playerResources = collision.GetComponentInParent<PlayerResources>();

            // Show the key pickup prompt
            UIManager.Instance.ShowPrompt("[E] Pick up " + keyType.ToString() + " Key");

            // collision.GetComponentInParent<PlayerResources>().collectKey();
            // Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;

            playerResources = null;

            // Hide the key pickup prompt
            UIManager.Instance.HidePrompt();
        }
    }

    private void PickupKey()
    {
        if (playerResources == null) return;

        if (keyType == KeyType.Silver)
            playerResources.CollectSilverKey();
        else
            playerResources.CollectGoldKey();

        UIManager.Instance.HidePrompt();

        Destroy(gameObject);
    }
}
