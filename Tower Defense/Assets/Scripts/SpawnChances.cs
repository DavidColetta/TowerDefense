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
        switch (wave){
            case 2:
                enemySpawner.spawnChances[1] = 0.4f;
                break;
            case 3:
                enemySpawner.spawnChances[2] = 0.2f;
                break;
            case 4:
                enemySpawner.spawnChances[1] = 0.7f;
                break;
            case 5:
                enemySpawner.spawnChances[0] = 0.7f;
                enemySpawner.spawnChances[3] = 0.4f;
                break;
            case 6:
                enemySpawner.spawnChances[4] = 0.4f;
                break;
            case 7:
                enemySpawner.spawnChances[0] = 0.2f;
                enemySpawner.spawnChances[2] = 0.1f;
                break;
            case 8:
                enemySpawner.spawnChances[5] = 0.2f;
                enemySpawner.spawnChances[6] = 0.4f;
                break;
            case 9: 
                enemySpawner.spawnChances[0] = 0f;
                enemySpawner.spawnChances[5] = 0.1f;
                break;
            default:
                break;
        }
    }
}
