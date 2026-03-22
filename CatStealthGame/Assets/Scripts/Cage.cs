using UnityEngine;

public class Cage : MonoBehaviour
{
    [Header("Cage Settings")]
    [SerializeField] private bool isFinalCage = false;
    [SerializeField] private GameObject OpenCagePrefab;

    private bool isUnlocked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only respond to player
        if (!collision.CompareTag("Player") || isUnlocked) return;

        // Get PlayerResources
        PlayerResources playerResources = collision.GetComponentInParent<PlayerResources>();
        if (playerResources == null) return;

        // Check if player has enough keys
        bool canUnlock = isFinalCage
            ? playerResources.HasGoldKeys()
            : playerResources.HasSilverKey();

        if (!canUnlock) return;

        // Unlock cage and consume keys
        isUnlocked = true;
        if (isFinalCage)
            playerResources.UseGoldKeys();
        else
            playerResources.UseSilverKey();

        // Spawn open cage prefab
        if (OpenCagePrefab != null)
        {
            Instantiate(OpenCagePrefab, transform.position, transform.rotation);
        }

        // Destroy the old cage object
        Destroy(gameObject);
    }
}