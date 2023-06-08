using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInGameUI : InGameUI
{
    public TowerPlace towerPlace;

    protected override void Awake()
    {
        base.Awake();

        buttons["Blocker"].onClick.AddListener(() => { GameManager.UI.CloseInGameUI(this); });
        buttons["ArchorButton"].onClick.AddListener(() => { BuildArchorTower(); });
        buttons["CanonButton"].onClick.AddListener(() => { BuildCanonTower(); });
    }

    public void BuildArchorTower()
    {
        TowerData archorTowerData = GameManager.Resource.Load<TowerData>("Data/ArchorTowerData");
        towerPlace.BuildTower(archorTowerData);
        GameManager.UI.CloseInGameUI(this);
    }

    public void BuildCanonTower()
    {
        TowerData canonTowerData = GameManager.Resource.Load<TowerData>("Data/CanonTowerData");
        towerPlace.BuildTower(canonTowerData);
        GameManager.UI.CloseInGameUI(this);
    }
}
