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
    private static Selector instance;
    
    void Awake()
    {
        selectedDisplayPanel.SetActive(false);
        instance = this;
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
                        Selector.SelectTower(selectedObject);
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
            selectedDisplayPanel.GetComponent<SelectedPanelTween>().Disable();
            if (rangeDisplay)
                rangeDisplay.SetActive(false);
        }
    }
    public static void SelectTower(GameObject _selected){
        if (selectedObject != _selected){
            selectedObject = _selected;
        }
        instance.selectedDisplayPanel.GetComponent<SelectedPanelTween>().Enable();
        instance.towerAI = _selected.GetComponent<TowerAI>();
        instance.nameDisplay.SetText(instance.towerAI.tower.name);
        if (instance.rangeDisplay)
            instance.rangeDisplay.SetActive(false);
        instance.rangeDisplay = _selected.transform.Find("Range").gameObject;
        instance.rangeDisplay.SetActive(true);
        if (instance.towerAI.tower.range > 0)
            instance.rangeDisplay.transform.localScale = Vector2.one*instance.towerAI.tower.range*2;
        if (instance.towerAI.tower.upgrade.Length > 0){
            instance.upgrade1Button.SetActive(true);
        }
    }
    public void ToolipSelectedTower(){
        if (towerAI != null){
            Tooltip.CreateTowerTooltip_Static(towerAI.tower);
        }
    }
}
