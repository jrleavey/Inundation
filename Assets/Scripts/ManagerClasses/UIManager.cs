using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private bool isTheGamePaused = false;
    public GameObject _pausemenu;

    public GameObject _mainFirstButton;
    public GameObject _optionsFirstButton;
    public GameObject _optionsClosedButton;
    public GameObject _creditsFirstButton;
    public GameObject _creditsClosedButton;

    public GameObject _optionsMenuSlide;
    public GameObject _creditsMenuSlide;

    [SerializeField]
    private Slider VolumeSlider;
    [SerializeField]
    private Slider BrightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;
    private void Start()
    {
        Instance = this;
        Time.timeScale = 1;
        isTheGamePaused = false;
    }

    public void PauseMenu()
    {
        isTheGamePaused = !isTheGamePaused;


        if (isTheGamePaused == true)
        {
            Time.timeScale = 0;
            _pausemenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_mainFirstButton);
        }
        else
        {
            Time.timeScale = 1;
            _pausemenu.SetActive(false);

        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        _pausemenu.SetActive(false);
    }    
    private void Update()
    {
        
    }
    public void AdjustVolume(float value)
    {
        AudioListener.volume = VolumeSlider.value;
        Save();
    }
    public void AdjustBrightness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = 0.5f;
        }
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", VolumeSlider.value);

    }
    public void Load()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }
    public void ChangeScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void OptionsMenu()
    {
        _optionsMenuSlide.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_optionsFirstButton);
    }
    public void CloseOptionsMenu()
    {
        _optionsMenuSlide.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_optionsClosedButton);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RetryGame()
    {
        // SceneManager.LoadScene
        //Load Restart the game
    }
}
