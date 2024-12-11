using UnityEngine;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image mute;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("audioVolume", 0.5f);
        AudioListener.volume = slider.value;
        CheckCalibration();

    }

    public void ChangeSlider(float value)
    {

        slider.value = value;
        PlayerPrefs.SetFloat("audioVolume", value);
        AudioListener.volume = slider.value;
        CheckCalibration();
    }   

    public void CheckCalibration()
    {

        if(slider.value != 0f)
        {

            mute.enabled = false;
        }
        else
        {

            mute.enabled = true;
        }
    }
}
