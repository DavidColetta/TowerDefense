using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int waveMultiplier = 100;
    public float spawnSpeedIncrease = 2f;
    public float spawnSpeed;
    public float ambushChance = 0.075f;
    public Vector2 spawnBox;
    public Enemy[] enemies;
    [HideInInspector]
    public SpawnChances spawnChanceScript;
    //[HideInInspector]
    public float[] spawnChances;
    [HideInInspector]
    public Vector2 spawnPos;
    public static int wave = 0;
    //[HideInInspector]
    public int spawnCurrency;
    [HideInInspector]
    public static bool waveInProgress = false;
    private List<int> cannotSpawn = new List<int>();
    private void Awake() {
        wave = 0;
        waveInProgress = false;

        spawnChances = new float[enemies.Length];
    }
    public void StartNextWave() {
        if (!waveInProgress){
            wave ++;
            waveInProgress = true;
            spawnCurrency = wave * waveMultiplier;
            spawnSpeed += spawnSpeedIncrease;
            cannotSpawn.Clear();

            AudioManager.Play_Static("Trumpets");
            
            float trueAmbushChance = 0;
            if (wave > 5)
                trueAmbushChance = (DifficultyManager.difficultyLevel)*ambushChance;
            
            if (trueAmbushChance > Random.value){
                spawnPos = new Vector2(spawnBox.x, Random.Range(-spawnBox.y, spawnBox.y));
                DifficultyManager.UpdateDifficulty(0.8f);
            }else{
                spawnPos = new Vector2(-spawnBox.x, 0);
                DifficultyManager.UpdateDifficulty();
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
            DifficultyManager.UpdateDifficulty(0.8f);

            AudioManager.Play_Static("Trumpets");

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
            if (0.25 > Random.value){
                spawnPos = new Vector2(-spawnBox.x, Random.Range(-spawnBox.y, spawnBox.y));
            }
        }
        WaveComplete();
    }
    private void WaveComplete(){
        waveInProgress = false;
        MoneyManager.GainMoney(Mathf.RoundToInt(waveMultiplier*DifficultyManager.localDifficulty));
        AudioManager.Play_Static("WaveComplete");
    }

    Enemy ChooseEnemy(){
        List<int> enemiesToCheck = new List<int>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!cannotSpawn.Contains(i)){
                if (enemies[i].spawnCost <= spawnCurrency && spawnChances[i] > 0){
                    if (spawnChances[i] >= 2){
                        spawnCurrency -= enemies[i].spawnCost;
                        return enemies[i];
                    }
                    enemiesToCheck.Add(i);
                } else {
                    cannotSpawn.Add(i);
                }
            }
        }
        while (enemiesToCheck.Count > 0)
        {
            int checkingID = enemiesToCheck[Random.Range(0, enemiesToCheck.Count)];
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
