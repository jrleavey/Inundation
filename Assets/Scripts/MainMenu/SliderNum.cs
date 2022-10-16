using TMPro;
using UnityEngine;

public class SliderNum : MonoBehaviour
{
    [Header("Option Slider Num Text")]
    public TextMeshProUGUI sliderNumber;

    public void SetSliderNum(float value)
    {
        sliderNumber.text = value.ToString();
    }
}
