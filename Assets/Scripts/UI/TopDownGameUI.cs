using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI coinCount;
    [SerializeField] private Slider hpSlider;



    private void Start()
    {

    }


    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }


    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    public void UpdateCoinText(float coin)
    {
        coinCount.text = $"Coin : {coin.ToString()}";
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
