using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSceneUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["InfoButton"].onClick.AddListener( () => { GameManager.UI.ShowWindowUI<WindowUI>("UI/Window"); } );
        buttons["VolumeButton"].onClick.AddListener( () => { Debug.Log("Volume"); } );
        buttons["SettingButton"].onClick.AddListener( () => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/SettingPopUpUI"); } );
    }
}
