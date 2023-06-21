using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPManager : MonoBehaviour
{
    public void GoldPurchase()
    {
        GameManager.instance.money += 2000 * (GameManager.instance.highstStage + 1);
        UIManager.Instance.ShowMoney();
    }
    public void DiaEighty()
    {
        GameManager.instance.dia += 80;
        UIManager.Instance.ShowDiaCount();
    }
    public void DiaFiveHundred()
    {
        GameManager.instance.dia += 500;
        UIManager.Instance.ShowDiaCount();
    }
    public void DiaElevenHundred()
    {
        GameManager.instance.dia += 1100;
        UIManager.Instance.ShowDiaCount();
    }
}
