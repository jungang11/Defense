using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoSceneUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake();

        texts["HeartText"].text = "100";
        texts["CoinText"].text = "0";
    }
}
