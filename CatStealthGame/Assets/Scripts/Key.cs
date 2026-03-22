using UnityEngine;

// Different types of keys the player can collect
public enum KeyType
{
    Silver,
    Gold,
}

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType; // Type of key

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only respond to the player
        if (!collision.CompareTag("Player")) return;

        // Get PlayerResources from the player
        PlayerResources playerResources = collision.GetComponentInParent<PlayerResources>();
        if (playerResources == null)
        {
            Debug.LogWarning("PlayerResources NOT found on player! Key cannot be collected.");
            return;
        }

        // Collect key
        if (keyType == KeyType.Silver)
            playerResources.CollectSilverKey();
        else
            playerResources.CollectGoldKey();

        // Destroy the key immediately
        Destroy(gameObject);
    }
}