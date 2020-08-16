using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool isGameOver = false;
    public float CheckGameoverRate = 3f;
    public static float NumberOfTowers;
    private void Awake() {
        isGameOver = false;
        InvokeRepeating("CheckIfGameover",CheckGameoverRate,CheckGameoverRate);
    }
    private void CheckIfGameover(){
        GameObject[] towers;
        towers = GameObject.FindGameObjectsWithTag("Tower");
        NumberOfTowers = towers.Length;
        if (NumberOfTowers == 0){
            if (EnemySpawner.wave > 0)
                Loose();
        }
    }
    public void Loose(){
        isGameOver = true;
        PauseManager.Pause_Static();

    }
}
