using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static int difficultyLevel = 1;
    public static float localDifficulty;
    public static float difficulty;
    public static void UpdateDifficulty(float difficultyMultiplier = 1f){
        localDifficulty = (1f + EnemySpawner.wave*0.1f);
        difficulty = (0.7f + difficultyLevel*0.3f) * localDifficulty * difficultyMultiplier;
    }
}
