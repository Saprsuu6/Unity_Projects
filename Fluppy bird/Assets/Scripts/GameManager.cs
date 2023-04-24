using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject menuCanvas;

    private GameObject resumeGameButton;
    private GameObject startGameButton;
    private GameObject gameOverText;
    private GameObject nightBackground;
    private GameObject dayBackground;

    // if gameOver TODO

    void Start()
    {
        resumeGameButton = GameObject.Find("ResumeGame");
        startGameButton = GameObject.Find("StartGame");
        gameOverText = GameObject.Find("GameOver");
        nightBackground = GameObject.Find("BackgroundNight");
        dayBackground = GameObject.Find("BackgroundDay");

        nightBackground.SetActive(false);
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        menuCanvas.SetActive(true);
        resumeGameButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Pause()
    {
        menuCanvas.SetActive(true);
        startGameButton.SetActive(false);
        gameOverText.SetActive(false);
        Time.timeScale = 0;
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        startGameButton.SetActive(true);
        gameOverText.SetActive(true);
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChangeTheme()
    {
        Debug.Log("Im here");

        //if (dayBackground.activeInHierarchy)
        //{
        //    dayBackground.SetActive(false);
        //    nightBackground.SetActive(true);
        //}
    }
}
