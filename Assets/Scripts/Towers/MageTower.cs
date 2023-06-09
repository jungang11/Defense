using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [SerializeField] Transform magicPoint;

    protected override void Awake()
    {
        base.Awake();

        data = GameManager.Resource.Load<TowerData>("Data/MageTowerData");
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
        Crystal crystal = GameManager.Resource.Instantiate<Crystal>("Tower/Crystal", magicPoint.position, magicPoint.rotation);
        crystal.SetTarget(enemy);
        crystal.SetDamage(data.Towers[0].damage);
    }
}
