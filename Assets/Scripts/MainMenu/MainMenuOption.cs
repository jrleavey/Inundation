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
        AdjustBrightness(brightnessSlider.value);
        AdjustVolume(volumeSlider.value);
    }
    public void AdjustVolume(float value)
    {
        volumeSliderNumber.text = (value+80).ToString();
        audioMixer.SetFloat("volume", value);
    }

    public void AdjustBrightness(float value)
    {
        
        brightnessSliderNumber.text = value.ToString();

        if(value!=0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = 0.05f;
        }
    }
}
