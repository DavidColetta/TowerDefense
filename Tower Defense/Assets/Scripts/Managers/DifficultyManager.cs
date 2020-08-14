using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public int _difficultyLevel = 2;
    public static int difficultyLevel;
    public static float localDifficulty;
    public static float difficulty;
    private void Awake() {
        difficultyLevel = _difficultyLevel;
        UpdateDifficulty();
    }
    public static void UpdateDifficulty(float difficultyMultiplier = 1f){
        localDifficulty = (0.5f + EnemySpawner.wave*0.1f);
        difficulty = (difficultyLevel*0.5f) * (0.5f+localDifficulty) * difficultyMultiplier;
    }
}
