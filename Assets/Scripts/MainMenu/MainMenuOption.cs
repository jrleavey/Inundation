using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MainMenuOption : MonoBehaviour
{
    [Header("Volume")]
    public Slider volumeSlider;
    public TextMeshProUGUI volumeSliderNumber;
    public AudioMixer audioMixer;

    [Header("Brightness")]
    public Slider brightnessSlider;
    public TextMeshProUGUI brightnessSliderNumber;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;


    private void Start()
    {
        brightness.TryGetSettings(out exposure);
        AdjustVolume(volumeSlider.value);
        AdjustVolume(brightnessSlider.value);
    }
    public void AdjustVolume(float value)
    {
        volumeSliderNumber.text = (value + 80).ToString();
        audioMixer.SetFloat("volume", value);
    }

    public void AdjustBrightness(float value)
    {
        if(value!=0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = .05f;

        }
    }
}
