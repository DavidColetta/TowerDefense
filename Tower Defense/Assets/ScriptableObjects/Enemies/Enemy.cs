using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Tower Defense/Enemy", order = 0)]
public class Enemy : ScriptableObject {
    public GameObject enemyObj;
    public new string name;
    public int maxHp;
    public int attackDmg;
    public float speed;
    public float attackRate;
    public float range;
    public int defense;
    public int spawnCost;
}