using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    private GameObject menu;

    #region Camera settings
    private Toggle toggleZoom;
    private Toggle toggleMouseVertical;
    private Slider sencitivitySlider;
    #endregion
    #region Sound settings
    private Slider sounds;
    private Slider music;
    private Toggle muteAll;
    #endregion
    #region Leader board
    private TMPro.TextMeshProUGUI leaderBoard;
    #endregion
    #region Difficulty settings
    private Toggle gameTimer;
    private Toggle coinDistance;
    private Toggle directionHint;
    private Toggle disapearTimer;
    private Toggle displayStamina;
    private TMPro.TextMeshProUGUI difficultyValue;
    private TMPro.TextMeshProUGUI coinPriceValue;
    #endregion

    void Start()
    {
        GameSettings.LoadSettings();

        toggleZoom = GameObject.Find("InvZoomToggle").GetComponent<Toggle>();
        toggleMouseVertical = GameObject.Find("InvVerticalToggle").GetComponent<Toggle>();
        sencitivitySlider = GameObject.Find("SensitivitySlider").GetComponent<Slider>();

        sounds = GameObject.Find("SoundsSlider").GetComponent<Slider>();
        music = GameObject.Find("MusicSlider").GetComponent<Slider>();
        muteAll = GameObject.Find("MuteAllSoundsToggle").GetComponent<Toggle>();

        directionHint = GameObject.Find("DirectionHintToggle").GetComponent<Toggle>();
        coinDistance = GameObject.Find("CoinDistanceToggle").GetComponent<Toggle>();
        gameTimer = GameObject.Find("GameTimerToggle").GetComponent<Toggle>();
        disapearTimer = GameObject.Find("DisapearTimerToggle").GetComponent<Toggle>();
        displayStamina = GameObject.Find("DisplayStaminaToggle").GetComponent<Toggle>();
        difficultyValue = GameObject.Find("DifficultyValue").GetComponent<TMPro.TextMeshProUGUI>();
        coinPriceValue = GameObject.Find("CoinPriceValue").GetComponent<TMPro.TextMeshProUGUI>();

        leaderBoard = GameObject.Find("BestBoardText").GetComponent<TMPro.TextMeshProUGUI>();

        ShowSettings();
        UpdateSettingsInReal();

        menu = GameObject.Find("MenuContent");
        menu.SetActive(false);
    }

    private void ShowSettings()
    {
        toggleZoom.isOn = GameSettings.InverseWheelZoom;
        toggleMouseVertical.isOn = GameSettings.InverseWheelZoom;
        sencitivitySlider.value = GameSettings.Sencitivity;

        sounds.value = GameSettings.Sounds;
        music.value = GameSettings.Music;
        muteAll.isOn = GameSettings.MuteAllSounds;

        gameTimer.isOn = GameSettings.DisplayGameTimer;
        coinDistance.isOn = GameSettings.DisplayCoinDistance;
        directionHint.isOn = GameSettings.DisplayDirectionHint;
        disapearTimer.isOn = GameSettings.DisplayDisaparTimer;
        displayStamina.isOn = GameSettings.DisplayStamina;

        leaderBoard.text = "";
        for (int i = 0; i < GameSettings.LeaderRecords.Count; i++)
        {
            var item = GameSettings.LeaderRecords[i];
            leaderBoard.text += $"{i + 1}. {item.Name} - {item.Score}\n";
        }
    }

    private void UpdateSettingsInReal()
    {
        difficultyValue.text = GameSettings.Difficulty.ToString();
        coinPriceValue.text = GameSettings.CoinPrice.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                menu.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void Exit()
    {
        if (EditorUtility.DisplayDialog("Really?", "Exit game?", "YES", "NO"))
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void DefaultSettings()
    {
        if (EditorUtility.DisplayDialog("Really?", "Restore default all settings?", "YES", "NO"))
        {
            GameSettings.RestoreDefaults();
            ShowSettings();
        }
    }

    public void Close()
    {
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void InverceWheelChanged(bool value)
    {
        GameSettings.InverseWheelZoom = value;
    }

    public void InverceVerticalChanged(bool value)
    {
        GameSettings.InverceMouseVertical = value;
    }
    public void SencetivityCahnged(float value)
    {
        GameSettings.Sencitivity = value;
    }
    public void DisplayGameTimerChanged(bool value)
    {
        GameSettings.DisplayGameTimer = value;
        UpdateSettingsInReal();
    }
    public void DisplayCoinDistanceChanged(bool value)
    {
        GameSettings.DisplayCoinDistance = value;
        UpdateSettingsInReal();
    }
    public void DisplayDirectionHintChanged(bool value)
    {
        GameSettings.DisplayDirectionHint = value;
        UpdateSettingsInReal();
    }
    public void DisplayDisapearTimerChanged(bool value)
    {
        GameSettings.DisplayDisaparTimer = value;
        UpdateSettingsInReal();
    }
    public void DisplayStaminaChanged(bool value)
    {
        GameSettings.DisplayStamina = value;
        UpdateSettingsInReal();
    }
    public void SoudChanged(float value)
    {
        GameSettings.Sounds = value;
        UpdateSettingsInReal();
    }

    public void MusicChanged(float value)
    {
        GameSettings.Music = value;
        UpdateSettingsInReal();
    }
    public void MuteAllChanged(bool value)
    {
        GameSettings.MuteAllSounds = value;
        UpdateSettingsInReal();
    }
}
