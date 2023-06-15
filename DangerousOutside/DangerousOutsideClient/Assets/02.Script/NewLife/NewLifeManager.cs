using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewLifeManager : MonoBehaviour
{
    static private NewLifeManager _instance;
    static public NewLifeManager Instance { get { return _instance; } }

    public TMP_Text newLifeText;
    public ulong soul;

    void Start()
    {
        if (_instance == null)
            _instance = this;

        soul = 0;
    }
    public ulong GetCurNewSoul()
    {
        int mult = (int)(( 1+ GameManager.instance.curStage) / 20);
        if(mult <= 0)
            return 0;
        double result = 3;
        for(int i =0;i < 5; i++)
        {
            result = 1.5 * result;
            mult--;
            if (mult <= 0)
                break;
        }
        for(;mult >0; mult--)
        {
            result = 1.2 * result;
            if (mult <= 0)
                break;
        }
        return (ulong)result;
    }
    public void SetText()
    {
        newLifeText.text = $"환생시 신체강화가 리셋됩니다.\r\n환생 소울\r\n{GetCurNewSoul()}개";
    }
    public void NewLifeButton()
    {
        if (GameManager.instance.curStage + 1 < 20)
            return;
        soul += GetCurNewSoul();
        UIManager.Instance.ShowSoul();
        GameManager.instance.curStage = 0;
        GameManager.instance.player.maxHealth = 10;
        CharStateManager.Instance.goldPowerLv = 0;
        CharStateManager.Instance.healthUpMoney = 10;
        UIManager.Instance.ShowPowerUpMoney();
        SetText();
        GameManager.instance.SetDamage();
        GameManager.instance.poolManager.Init(0);
        GameManager.instance.curStage = 0;
        GameManager.instance.spawner.SetMonster();
        GameManager.instance.spawner.monsterCount = 0;
        UIManager.Instance.ShowStage();

    }
}
