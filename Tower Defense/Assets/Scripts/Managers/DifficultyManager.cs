using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public int difficultyLevel = 2;
    public static float localDifficulty;
    public static float difficulty;

    void Update()
    {
        localDifficulty = (0.9f + EnemySpawner.wave*0.1f);
        difficulty = (difficultyLevel*0.5f) * localDifficulty;
    }
}
