using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : MonoBehaviour
{
    public FeedbackPopup popup;
    public TextMeshProUGUI timerText;
    public GameObject shieldIconFill;
    public Image staminaIconFill;
    public Color colorVidas;
    public TextMeshProUGUI huesitosTXT;

    public List<Image> livesIMGs;

    public string FloatToTimeFormat(float _time)
    {
        var ts = TimeSpan.FromSeconds(_time);
        return string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }
    public void UpdateTimerTEXT(float _time)
    {
        timerText.text = FloatToTimeFormat(_time);
    }

    public void UpdateHuesitosTEXT(int _huesitos)
    {
        huesitosTXT.text = $"x{_huesitos}";
    }

    public void UpdateStaminaFill(float _actual, float _max)
    {
        staminaIconFill.fillAmount = (_max / _actual);
    }

    public void UpdateVidas(int _vidas)
    {
        //PONEMOS TODAS BLANCAS
        foreach(Image _vidaIcon in livesIMGs) {
            _vidaIcon.color = Color.white;
        }

        //SOLO COLOREAMOS LA CANTIDAD SEGUN VIDAS
        for(int i=0; i<_vidas; i++)
        {
            livesIMGs[i].color = colorVidas;
        }

    }
    public void ToggleShieldIcon(bool _isOn)
    {
        shieldIconFill.SetActive(_isOn);
    }
}
