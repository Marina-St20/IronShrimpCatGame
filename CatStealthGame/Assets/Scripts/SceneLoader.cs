using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

     // Load the start screen when the player wants to return to the main menu
    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    // Load the end scene when the player wins
    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScreen");
    }

    // Load the end scene when the player loses
    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    // Allow the player to quit the game from the end screen
    public void QuitGame()
    {
        Application.Quit();
    }
}
