using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
    

    private void Start()
    {
        AdjustVolume(volumeSlider.value);
    }
    public void AdjustVolume(float value)
    {
        volumeSliderNumber.text = (value + 80).ToString();
        audioMixer.SetFloat("volume", value);
    }
}
