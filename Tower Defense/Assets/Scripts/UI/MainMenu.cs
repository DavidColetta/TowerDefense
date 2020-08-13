using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedPanel = null;
    private void Awake() {
        selectedPanel.SetActive(true);
    }
    public void GoToPanel(GameObject panel){
        selectedPanel.SetActive(false);
        selectedPanel = panel;
        selectedPanel.SetActive(true);
    }
}
