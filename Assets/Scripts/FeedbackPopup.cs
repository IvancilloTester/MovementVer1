using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackPopup : MonoBehaviour
{

    public TextMeshProUGUI popupTXT;
    public Image popupImg;

    public Sprite staminaIcon;
    public Color staminaColor;
    public Sprite lifeIcon;
    public Color lifeColor;
    public Sprite timeIcon;
    public Color timeColor;
    public Sprite shieldIcon;
    public Color shieldColor;
    public Sprite huesitoIcon;
    public Color huesitoColor;

    private void Start()
    {
        HidePopup();
    }
    public void ShowPopup(string _text, string _type)
    {
        popupTXT.text = _text;

        switch (_type)
        {
            case "stamina":
                popupImg.sprite = staminaIcon;
                popupImg.color = staminaColor;
                break;
            case "life":
                popupImg.sprite = lifeIcon;
                popupImg.color = lifeColor;
                break;
            case "time":
                popupImg.sprite = timeIcon;
                popupImg.color = timeColor;
                break;
            case "shield":
                popupImg.sprite = shieldIcon;
                popupImg.color = shieldColor;
                break;
            case "huesito":
                popupImg.sprite = huesitoIcon;
                popupImg.color = huesitoColor;
                break;
        }

        popupImg.gameObject.SetActive(true);

        Invoke("HidePopup", 3f);

    }

    private void HidePopup()
    {
        popupTXT.text = "";
        popupImg.gameObject.SetActive(false);
    }

}
