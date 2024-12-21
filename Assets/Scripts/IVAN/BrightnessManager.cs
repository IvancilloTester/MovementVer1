using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager instance;
    public Image brightnessPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        float savedBrightness = PlayerPrefs.GetFloat("brightness", 0.5f);
        ApplyBrightness(savedBrightness);
    }

    public void ApplyBrightness(float value)
    {
        PlayerPrefs.SetFloat("brightness", value);
        if (brightnessPanel != null)
        {
            brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, value);
        }
        else
        {
            Debug.LogWarning("Brightness panel is not assigned.");
        }
    }
}
