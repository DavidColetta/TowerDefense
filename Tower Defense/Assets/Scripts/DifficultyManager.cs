using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public int difficultyLevel = 2;
    private EnemySpawner enemySpawner;
    public float difficulty;
    void Start()
    {
        enemySpawner = transform.Find("EnemySpawner").gameObject.GetComponent<EnemySpawner>();
    }

    void Update()
    {
        difficulty = (difficultyLevel*0.5f) * (0.9f + enemySpawner.wave*0.1f);
    }
}
