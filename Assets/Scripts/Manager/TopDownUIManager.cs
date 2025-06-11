using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Home,
    Game,
    GameOver
}

public class UIManager : MonoBehaviour
{
    HomeUI homeUI;
    GameUI gameUI;
    GameOverUI gameOverUI;
    MainMenuUI mainMenuUI;
    InventoryUI inventoryUI;
    StatusUI statusUI;

    private StatData statData;

    private UIState currentState;

    private void Awake()
    {
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);
        mainMenuUI = GetComponentInChildren<MainMenuUI>(true);
        mainMenuUI.Init(this);
        inventoryUI = GetComponentInChildren<InventoryUI>(true);
        inventoryUI.Init(this);
        statusUI = GetComponentInChildren<StatusUI>(true);
        statusUI.Init(this);

        ChangeState(UIState.Home);


    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void ChangeWave(int waveIndex)
    {
        gameUI.UpdateWaveText(waveIndex);
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        gameUI.UpdateHPSlider(currentHP / maxHP);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
        mainMenuUI.SetActive(currentState);
        inventoryUI.SetActive(currentState);
        statusUI.SetActive(currentState);
    }

    public void CoinCount(float coin)
    {
        gameUI.UpdateCoinText(coin);
    }
}
