using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] Vector3 spawnPoint;
    EnemyManager enemyManager;

    bool alreadySpawned = false;

    private void Start()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (alreadySpawned)
            return;

        alreadySpawned = true;

        enemyManager.SpawnBoss(spawnPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(spawnPoint, .3f);
    }
}

