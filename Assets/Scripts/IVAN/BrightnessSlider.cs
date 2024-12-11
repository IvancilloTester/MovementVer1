using UnityEngine;
using UnityEngine.UI;


public class BrightnessSlider : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image brightnessPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void brightnessScene(float value)
    {


    }

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brightness", 0.5f);

        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, slider.value);

    }

    public void ChangeSlider(float value)
    {

        slider.value = value;
        PlayerPrefs.SetFloat("brightness", value);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, slider.value);
    }
}
