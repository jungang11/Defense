using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["ContinueButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
        buttons["SettingButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/ConfigPopUpUI"); });
        buttons["ExitButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
    }
}
