using UnityEngine;

public class Cage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (collision.GetComponentInParent<PlayerResources>().keys > 0))
        {
            collision.GetComponentInParent<PlayerResources>().openCage();
            // Temp Destroy
            Destroy(gameObject);
        }
    }
}
