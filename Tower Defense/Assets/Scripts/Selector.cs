using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Selector : MonoBehaviour
{
    public static GameObject selectedObject;
    public LayerMask clickableLayers;
    public GameObject selectedDisplayPanel;
    public TextMeshProUGUI nameDisplay;
    public TextMeshProUGUI hpDisplay;
    private TowerAI towerAI;
    private GameObject rangeDisplay;
    public GameObject upgrade1Button;
    
    void Awake()
    {
        selectedDisplayPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!EventSystem.current.IsPointerOverGameObject()){
                Vector2 mousePos2D = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 0f, clickableLayers);
                if (hit.collider != null) {
                    selectedObject = hit.collider.gameObject;
                    if (selectedObject.tag == "Tower" || selectedObject.tag == "Wall"){
                        selectedDisplayPanel.SetActive(true);
                        towerAI = selectedObject.GetComponent<TowerAI>();
                        nameDisplay.SetText(towerAI.tower.name);
                        if (rangeDisplay)
                            rangeDisplay.SetActive(false);
                        rangeDisplay = selectedObject.transform.Find("Range").gameObject;
                        rangeDisplay.SetActive(true);
                        if (towerAI.tower.range > 0)
                            rangeDisplay.transform.localScale = Vector2.one*towerAI.tower.range*2;
                        if (towerAI.tower.upgrade.Length > 0){
                            upgrade1Button.SetActive(true);
                        }
                    }
                } else {
                    selectedObject = null;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace)){
            selectedObject = null;
        }
        if (selectedObject){
            hpDisplay.SetText(towerAI.hp.ToString() + "/" + towerAI.tower.maxHp.ToString());
        } else {
            selectedDisplayPanel.SetActive(false);
            if (rangeDisplay)
                rangeDisplay.SetActive(false);
        }
    }
    public void ToolipSelectedTower(){
        if (towerAI != null){
            Tooltip.CreateTowerTooltip_Static(towerAI.tower);
        }
    }
}
