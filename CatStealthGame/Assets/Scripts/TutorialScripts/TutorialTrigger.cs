using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [TextArea]
    public string message;
    public bool isHazardTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TutorialManager manager = Object.FindFirstObjectByType<TutorialManager>();
            if (manager != null)
            {
                manager.ShowMessage(message);

                Destroy(gameObject, 3f);
            }
        }
    }
    private void OnDestroy()
    {
        if (isHazardTrigger)
        {
            TutorialManager manager = Object.FindFirstObjectByType<TutorialManager>();
            if (manager != null)
            {
                manager.HideMessage();
            }
        }
    }
}