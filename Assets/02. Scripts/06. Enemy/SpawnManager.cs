using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnTime;
    [SerializeField] GameObject enemyPrefab;

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            GameManager.Resource.Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
