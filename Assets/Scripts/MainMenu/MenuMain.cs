using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class MenuMain : MonoBehaviour
{

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
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
        }
        else
        {
            Load();
        }

        brightness.TryGetSettings(out exposure);
        AdjustBrightness(BrightnessSlider.value);
    }
    public void Play() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void Quit()
    {
        Application.Quit();
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

    public void CreditsMenu()
    {
        _creditsMenuSlide.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_creditsFirstButton);
    }
    public void CloseCreditsMenu()
    {
        _creditsMenuSlide.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_creditsClosedButton);
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
}
