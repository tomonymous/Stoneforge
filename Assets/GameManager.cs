
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameOverState = false;
    public bool resume = false;
    public float restartDelay = 5f;
    public GameObject gameOverMenu;
    public GameObject modeMenu;
    public GameObject shopMenu;
    public GameObject pauseMenu;
    public Button settingsButton;
    public Button gameOverRestartButton;
    public Button gameOverMenuButton;

    public void GameOver()
    {
        if (gameOverState == false)
        {

            gameOverState = true;
            gameOverMenu.SetActive(true);
            gameOverMenuButton.interactable.Equals(false);
            gameOverRestartButton.interactable.Equals(false);
            //Invoke("Restart", restartDelay);
        }
    }

    public void EnableEndgameButtons()
    {
        if (gameOverState)
        {
            gameOverMenuButton.interactable = true;
            gameOverRestartButton.interactable = true;
        }
    }

    public void Restart()
    {
        if (FindObjectOfType<ComboMatch>().chaosMode)
        {
            FindObjectOfType<AudioManager>().PlayThemeMusic();
        }

        SceneManager.LoadScene(1);
    }

    public void SaveAndExit()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        FindObjectOfType<ComboMatch>().SaveGame();
        SceneManager.LoadScene(0);
    }

    public void StartPlaying()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        gameOverMenu.SetActive(false);
        shopMenu.SetActive(false);
        settingsButton.interactable = true;
        CountdownTimer.timerPaused = false;
        CountdownTimer.timerRunning = true;
        MovePieces.instance.StartTheGame();
    }

    public void ResumeFromSave()
    {
        gameOverMenu.SetActive(false);
        shopMenu.SetActive(false);
        settingsButton.interactable = true;
        CountdownTimer.timerPaused = false;
    }

    public void Pause()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        FindObjectOfType<ComboMatch>().SaveGame();
        CountdownTimer.timerPaused = true;
        pauseMenu.SetActive(true);
        shopMenu.SetActive(false);
        MovePieces.instance.Pause();

    }
    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        pauseMenu.SetActive(false);
        if (!FindObjectOfType<ComboMatch>().zenMode)
        {
            CountdownTimer.timerRunning = true;
        }
        MovePieces.instance.Resume();

    }
    public void ShopToMenu()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        PlayerPrefs.SetInt("GameSaved", 0);
        SceneManager.LoadScene(0);
    }
    public void ToMenu()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        MovePieces.instance.Pause(); //to save inventory
        SceneManager.LoadScene(0);
    }
    public void EndGame()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        pauseMenu.SetActive(false);
        MovePieces.instance.EndGame(); 
    }
}
