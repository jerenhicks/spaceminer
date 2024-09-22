using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    public static bool isPaused = false;
    public static GameObject selectedObject = null;

    public GameObject player;
    public GameObject playerCorvette;
    public GameObject playerFighter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        player.gameObject.SetActive(false);
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        PlayerMovement fighterMovement = playerFighter.GetComponent<PlayerMovement>();
        fighterMovement.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}