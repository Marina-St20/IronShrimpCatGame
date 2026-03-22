using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [TextArea]
    public string message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TutorialManager manager = Object.FindFirstObjectByType<TutorialManager>();
            if (manager != null)
            {
                manager.ShowMessage(message);
            }
        }
    }
}