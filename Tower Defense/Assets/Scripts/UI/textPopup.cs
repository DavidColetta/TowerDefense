using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textPopup : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public string text = "";
    public int value;
    public bool isBold = false;
    [HideInInspector]
    public float dissapearTimer = 1f;
    public Vector2 moveSpeed;
    public Color textColor;
    public Color boldColor;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start() {
        textMesh = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (value > 0){
            text = "+";
        } else if (value < 0){
            text = "";
        } else {
            return;
        }

        if (isBold){
            textMesh.fontSize = 30;
            textMesh.fontStyle = FontStyles.Bold;
            textColor = boldColor;
        }

        textMesh.SetText(text + value.ToString());
        textMesh.color = textColor;
    }

    private void FixedUpdate() {
        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer<0){
            Destroy(gameObject);
        }

        rectTransform.anchoredPosition += moveSpeed * Time.deltaTime;
    }
}