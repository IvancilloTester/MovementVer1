using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brightness", 0.5f);
        slider.onValueChanged.AddListener(ChangeSlider);
    }

    public void ChangeSlider(float value)
    {
        BrightnessManager.instance.ApplyBrightness(value); //Aqui llamo a la instance del brightnessmanager NO OLVIDAR
    }
}
