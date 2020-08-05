using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowerPlacer : MonoBehaviour
{
    public GameObject towerPlacer;
    public void SpawnTowerButtonPressed(Tower tower){
        if (!tower){
            Debug.LogWarning("Tower not found!");
            return;
        }
        GameObject _TowerPlacer = Instantiate(towerPlacer, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, transform);
        _TowerPlacer.GetComponent<TowerPlacement>().tower = tower;
    }
}
