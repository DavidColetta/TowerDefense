using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    public string text1;
    public string text2;
    private TextMeshProUGUI textDisplay;
    private void Awake() {
        textDisplay = GetComponent<TextMeshProUGUI>();
        textDisplay.text = text1 + Application.version + text2;
    }
}
