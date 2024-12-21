using UnityEngine;

public class ApplyBrightnessOnStart : MonoBehaviour
{
    private void Start()
    {
        if (BrightnessManager.instance != null)
        {
            float savedBrightness = PlayerPrefs.GetFloat("brightness", 0.5f);
            BrightnessManager.instance.ApplyBrightness(savedBrightness);
        }
    }
}
