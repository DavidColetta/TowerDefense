using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Tower Defense/Tower")]
public class Tower : ScriptableObject {
    public GameObject towerObj;
    public new string name;
    public Sprite sprite;
    public int price;
    public int level = 1;

    public int maxHp;
    public int attackDmg;
    public float attackRate;
    public float range;

    public Tower[] upgrade;
}