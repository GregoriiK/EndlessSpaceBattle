using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> wavesConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    WaveConfigSO currentWave;
    void Start()
    {
        StartCoroutine(WavesOfEnemies());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i<currentWave.GetEnemyCount(); i++)
        {
            Instantiate(currentWave.GetEnemyPrefab(0), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0,0,180), transform);
            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }
    }

    IEnumerator WavesOfEnemies()
    {
        do
        {
            foreach (WaveConfigSO child in wavesConfigs)
            {
                currentWave = child;
                StartCoroutine(SpawnEnemies());
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }
}
