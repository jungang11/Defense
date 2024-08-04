using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed;

    private EnemyController enemy;
    private int damage;
    private Vector3 targetPoint;

    public void SetTarget(EnemyController enemy)
    {
        this.enemy = enemy;
        StartCoroutine(ArrowRoutine());
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    IEnumerator ArrowRoutine()
    {
        while (true)
        {
            if(enemy != null)
                targetPoint = enemy.transform.position;

            transform.LookAt(targetPoint);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

            if(Vector3.Distance(targetPoint, transform.position) < 0.1f)
            {
                if(enemy != null)
                    Attack(enemy);
                GameManager.Resource.Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }

    public void Attack(EnemyController enemy)
    {
        enemy.TakeHit(damage);
    }
}
