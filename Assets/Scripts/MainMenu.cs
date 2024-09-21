using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene in the build settings
        UnityEngine.SceneManagement.SceneManager.LoadScene("spaceminer");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
