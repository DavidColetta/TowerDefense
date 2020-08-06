using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int startingMoney;
    public static int money;
    public TextMeshProUGUI moneyDisplayTMP;
    public GameObject renderCanvas;
    public GameObject moneyPopup;
    public Vector2 moneyPopupPos = new Vector2(56.305f, -58f);
    private GameObject _moneyPopup;
    private static MoneyManager instance;
    void Awake()
    {
        moneyDisplayTMP.SetText("$" + money.ToString());
        instance = this;
        money = 0;
        GainMoney(startingMoney);
    }
    void Update()
    {
        
    }
    public void GainMoneyFunc(int amount){
        money += amount;
        moneyDisplayTMP.SetText("$" + money.ToString());
        if (!_moneyPopup){
            _moneyPopup = Instantiate(moneyPopup, moneyPopupPos, Quaternion.identity);
            _moneyPopup.transform.SetParent(renderCanvas.transform, false);
        }
        _moneyPopup.GetComponent<RectTransform>().anchoredPosition = moneyPopupPos;
        textPopup _moneyPopupScript = _moneyPopup.GetComponent<textPopup>();
        _moneyPopupScript.value += amount;
        _moneyPopupScript.dissapearTimer = 1f;

        if (amount >= 60){
            _moneyPopupScript.isBold = true;
        }
    }
    public static void GainMoney(int amount){
        instance.GainMoneyFunc(amount);
    }
}
