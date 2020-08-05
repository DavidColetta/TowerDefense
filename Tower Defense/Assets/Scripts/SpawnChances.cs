using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChances : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        enemySpawner.spawnChanceScript = this;
    }

    public void UpdateSpawnChances(int wave){
        if (wave == 2){
            enemySpawner.spawnChances[1] = 0.4f;
        }
        if (wave == 3){
            enemySpawner.spawnChances[2] = 0.2f;
        }
        if (wave == 6){
            enemySpawner.spawnChances[1] = 0.7f;
            enemySpawner.spawnChances[3] = 0.4f;
        }
        if (wave == 7){
            enemySpawner.spawnChances[0] = 0.7f;
        }
        if (wave == 8){
            enemySpawner.spawnChances[2] = 0.1f;
        }
        if (wave == 10){
            enemySpawner.spawnChances[0] = 0.2f;
            enemySpawner.spawnChances[4] = 0.4f;
        }
    }
}
