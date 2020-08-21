using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static int difficultyLevel = 2;
    public static float localDifficulty;
    public static float difficulty;
    public static void UpdateDifficulty(float difficultyMultiplier = 1f){
        localDifficulty = (0.5f + EnemySpawner.wave*0.1f);
        difficulty = (difficultyLevel*0.5f) * (0.5f+localDifficulty) * difficultyMultiplier;
    }
}
