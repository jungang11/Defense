using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackRange : MonoBehaviour
{
    public LayerMask enemyMask;

    public UnityEvent<EnemyController> OnInRangeEnemy;
    public UnityEvent<EnemyController> OnOutRangeEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (enemyMask.IsContain(other.gameObject.layer))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.OnDied.AddListener(() => { OnOutRangeEnemy?.Invoke(enemy); });
            OnInRangeEnemy?.Invoke(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemyMask.IsContain(other.gameObject.layer))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            OnOutRangeEnemy?.Invoke(enemy);
        }
    }
}
