using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float time;

    private int damage;
    private Vector3 targetPoint;

    public void SetTarget(EnemyController enemy)
    {
        targetPoint = enemy.transform.position;
        StartCoroutine(CanonRoutine());
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    IEnumerator CanonRoutine()
    {
        // 포물선에서 수평속도는 초기 속도
        float xSpeed = (targetPoint.x - transform.position.x) / time;
        float zSpeed = (targetPoint.z - transform.position.z) / time;
        // y의 속도 (S + (-1/2 * a(가속도) * t^2 (시간의 제곱))) / time
        float ySpeed = -1 * (0.5f * Physics.gravity.y * time * time + transform.position.y) / time;

        float curTime = 0;
        while (curTime < time)
        {
            curTime += Time.deltaTime;

            transform.position += new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime;

            ySpeed += Physics.gravity.y * Time.deltaTime;
            yield return null;
        }

        Explosion();
        GameManager.Resource.Destroy(gameObject);
    }

    public void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));
        foreach(Collider collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();
            enemy?.TakeHit(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
