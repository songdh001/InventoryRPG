using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BaseUI
{
    [SerializeField] private Button returnButton;
    UIManager uIManager;

    protected override UIState GetUIState()
    {
        throw new System.NotImplementedException();
    }

    public void OnClickreturnButton()
    {
        uIManager.ChangeState(GetUIState());
    }
}
