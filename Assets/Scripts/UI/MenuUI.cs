using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject gameWinMenu;
    
    public SO_Inventory inventory;
    
    [SerializeField]private VoidParameterEventChannel GameOverEvent;
    [SerializeField]private VoidParameterEventChannel GameWinEvent;
    private void Awake()
    {
        GameOverEvent.AddListener(GameOver);
        GameWinEvent.AddListener(GameWin);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                PauseGame();
            }
            else
            {
                BackGame();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
        
        inventory.ClearInventory();
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }

    public void BackGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StartScene");
        
        inventory.ClearInventory();
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        inventory.ClearInventory();
        
        Application.Quit();
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        
        Time.timeScale = 0f;
    }
    
    public void GameWin()
    {
        gameWinMenu.SetActive(true);
            
        Time.timeScale = 0f;
    }
}
