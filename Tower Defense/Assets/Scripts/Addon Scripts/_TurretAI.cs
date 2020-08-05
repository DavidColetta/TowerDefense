using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TurretAI : MonoBehaviour
{
    private TowerAI towerAI;
    private GameObject childCannon;
    // Start is called before the first frame update
    void Start()
    {
        towerAI = GetComponent<TowerAI>();
        childCannon = transform.Find("CannonRotation").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        childCannon.transform.rotation = towerAI.dir;
    }
}
