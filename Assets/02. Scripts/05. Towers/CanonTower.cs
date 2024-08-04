using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower
{
    [SerializeField] Transform canonPoint;

    protected override void Awake()
    {
        base.Awake();

        data = GameManager.Resource.Load<TowerData>("Data/CanonTowerData");
    }

    private void OnEnable()
    {
        StartCoroutine(AttackRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (enemyList.Count > 0)
            {
                Attack(enemyList[0]);
                yield return new WaitForSeconds(data.Towers[0].delay);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void Attack(EnemyController enemy)
    {
        CanonBall canon = GameManager.Resource.Instantiate<CanonBall>("Tower/CanonBall", canonPoint.position, canonPoint.rotation);
        canon.SetTarget(enemy);
        canon.SetDamage(data.Towers[0].damage);
    }
}
