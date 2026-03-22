using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;

    public void ShowMessage(string message)
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = message;
    }

    public void HideMessage()
    {
        tutorialPanel.SetActive(false);
    }
}