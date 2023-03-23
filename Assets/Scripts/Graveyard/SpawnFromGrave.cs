using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class SpawnFromGrave : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] Grave[] graves;

    [Header("Difficulty curve")]
    [SerializeField] float baseSpawnRate = 2;
    [SerializeField] float spawnRatePerNightFactor = 0.97f;
    [SerializeField] float spawnRateDeviationFactor = 0.2f;

    private float spawnTimer;
    private float spawnRate;
    private bool isNight = false;
    private bool isSpawned = false;

    private void Start()
    {
        DayNightCycle.dayStart += ChangeState;
        DayNightCycle.nightStart += ChangeState;

        graves = (Grave[]) GameObject.FindObjectsOfType(typeof(Grave));

        spawnRate = baseSpawnRate;
    }

    private void OnDestroy()
    {
        DayNightCycle.dayStart -= ChangeState;
        DayNightCycle.nightStart -= ChangeState;
    }

    private void Update()
    {
        if (!isNight) return;

        spawnTimer -= Time.deltaTime;
        
        if(spawnTimer <= 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        spawnTimer = spawnRate * Random.Range(1 - spawnRateDeviationFactor, 1 + spawnRateDeviationFactor);

        while (!isSpawned)
        {
            isSpawned = graves[Random.Range(0, graves.Length)].SpawnMonster();
        }

        isSpawned = false;
    }

    void ChangeState()
    {
        if (isNight)
        {
            spawnRate = spawnRate * spawnRatePerNightFactor;
        }

        isNight = !isNight;
    }
}
