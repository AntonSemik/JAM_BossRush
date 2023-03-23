using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class SpawnFromCrypts : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] Crypt[] crypts;
    [SerializeField] int activeFromNight;
    [SerializeField] int maxIterations = 10;
    int iterations;
    bool isActive = false;

    [Header("Difficulty curve")]
    [SerializeField] float baseSpawnRate = 2;
    [SerializeField] float spawnRatePerNightFactor = 0.97f;
    [SerializeField] float spawnRateDeviationFactor = 0.2f;

    private float spawnTimer;
    private float spawnRate;
    private bool isNight = false;
    private bool isSpawned = false;

    private void Awake()
    {
        DayNightCycle.updateNightCountUI += CheckNight;
        DayNightCycle.dayStart += ChangeState;
        DayNightCycle.nightStart += ChangeState;
    }
    private void Start()
    {
        crypts = (Crypt[]) GameObject.FindObjectsOfType(typeof(Crypt));

        spawnRate = baseSpawnRate;
    }

    private void OnDestroy()
    {
        DayNightCycle.dayStart -= ChangeState;
        DayNightCycle.nightStart -= ChangeState;

        DayNightCycle.updateNightCountUI -= CheckNight;
    }

    private void Update()
    {
        if (!isNight || !isActive) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnEnemy();
        }
    }
    void CheckNight(int night)
    {
        if (night >= activeFromNight)
        {
            isActive = true;
        }
    }


    void SpawnEnemy()
    {
        spawnTimer = spawnRate * Random.Range(1 - spawnRateDeviationFactor, 1 + spawnRateDeviationFactor);

        iterations = 0;
        while (!isSpawned)
        {
            isSpawned = crypts[Random.Range(0, crypts.Length)].SpawnMonster();
            iterations++;
            if (iterations >= maxIterations) break;
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
