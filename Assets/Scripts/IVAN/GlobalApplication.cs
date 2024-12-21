using UnityEngine;

public class GlobalBrightness : MonoBehaviour
{
    public static float BrightnessValue = 0.5f;

    private void Start()
    {
        BrightnessValue = PlayerPrefs.GetFloat("brightness", 0.5f);
        ApplyBrightness(BrightnessValue);
    }

    public void ApplyBrightness(float value)
    {
        BrightnessValue = value;
        PlayerPrefs.SetFloat("brightness", value);
    }
}
