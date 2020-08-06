using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int waveMultiplier = 100;
    public float spawnSpeed;
    public Vector2 spawnBox;
    public Enemy[] enemies;
    [HideInInspector]
    public SpawnChances spawnChanceScript;
    public float[] spawnChances;
    [HideInInspector]
    public Vector2 spawnPos;
    public static int wave = 0;
    [HideInInspector]
    public int spawnCurrency;
    [HideInInspector]
    public static bool waveInProgress = false;
    private List<int> cannotSpawn = new List<int>();
    private void Awake() {
        wave = 0;
        waveInProgress = false;
    }
    public void StartNextWave() {
        if (!waveInProgress){
            wave ++;
            waveInProgress = true;
            spawnCurrency = wave * waveMultiplier;
            spawnSpeed ++;
            cannotSpawn.Clear();
            RestartWaveButton.CanRestart = true;
            
            spawnPos = new Vector2(Random.Range(-spawnBox.x, spawnBox.x), Random.Range(-spawnBox.y, spawnBox.y));
            if (Random.value > 0.5f){
                spawnPos = new Vector2(spawnBox.x*(spawnPos.x / Mathf.Abs(spawnPos.x)), spawnPos.y);
            } else {
                spawnPos = new Vector2(spawnPos.x, spawnBox.y*(spawnPos.y / Mathf.Abs(spawnPos.y)));
            }
            spawnChanceScript.UpdateSpawnChances(wave);
            StartCoroutine("SpawnWave");
        }
    }
    public void RestartWave(){
        if (!waveInProgress){
            waveInProgress = true;
            spawnCurrency = wave * waveMultiplier;
            cannotSpawn.Clear();
            RestartWaveButton.CanRestart = false;

            StartCoroutine("SpawnWave");
        }
    }
    IEnumerator SpawnWave(){
        while (cannotSpawn.Count < enemies.Length)
        {
            Enemy _enemyToSpawn = ChooseEnemy();
            if (_enemyToSpawn){
                SpawnEnemy(_enemyToSpawn);
                yield return new WaitForSeconds(_enemyToSpawn.spawnCost / spawnSpeed);
            }
            //spawnPos += new Vector2(1,0);
        }
        waveInProgress = false;

        MoneyManager.GainMoney(Mathf.RoundToInt(waveMultiplier*(0.5f + wave*0.1f)));
    }

    Enemy ChooseEnemy(){
        List<int> enemiesToCheck = new List<int>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!cannotSpawn.Contains(i)){
                if (enemies[i].spawnCost <= spawnCurrency && spawnChances[i] > 0){
                    enemiesToCheck.Add(i);
                } else {
                    cannotSpawn.Add(i);
                }
            }
        }
        while (enemiesToCheck.Count > 0)
        {
            int checkingID = enemiesToCheck[Random.Range(0, enemiesToCheck.Count)];
            //Debug.Log(checkingID);
            if (spawnChances[checkingID] > Random.value){
                spawnCurrency -= enemies[checkingID].spawnCost;
                return enemies[checkingID];
            } else {
                enemiesToCheck.Remove(checkingID);
            }
        } return null;
    }
    void SpawnEnemy(Enemy enemyToSpawn){
        if (enemyToSpawn){
            Instantiate(enemyToSpawn.enemyObj, spawnPos+new Vector2(Random.Range(-2f,2f),Random.Range(-2f,2f)), Quaternion.identity, transform);
        }
    }
}
