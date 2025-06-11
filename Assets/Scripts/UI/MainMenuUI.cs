using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;
    public InventoryUI inventoryUI;
    public StatusUI statusUI;

    public TextMeshProUGUI iDText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;

    UIManager uIManager;
    protected override UIState GetUIState()
    {
        throw new System.NotImplementedException();
    }

    public void OnClickInventoryButton()
    {
        uIManager.ChangeState(GetUIState());
    }

    public void OnClickStatusButton()
    {
        uIManager.ChangeState(GetUIState());
    }
}
