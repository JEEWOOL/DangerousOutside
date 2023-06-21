using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class NewLifeManager : MonoBehaviour
{
    static private NewLifeManager _instance;
    static public NewLifeManager Instance { get { return _instance; } }

    public TMP_Text newLifeText;
    public ulong soul;

    private LocalizedString newLifeDes;
    private LocalizedString soulStone;

    void Start()
    {
        if (_instance == null)
            _instance = this;

        soul = 0;
        newLifeDes = new LocalizedString("UITAB", "NewLifeDes");
        soulStone = new LocalizedString("UITAB", "SoulStone");
    }
    public ulong GetCurNewSoul()
    {
        int mult = (int)(( 1+ GameManager.instance.curStage) / 20);
        if(mult <= 0)
            return 0;
        double result = 5;
        for(int i =0;i < 10; i++)
        {
            if (mult <= 0)
                break;
            result = 1.5 * result;
            mult--;
        }
        for(int i =0;i < 10; i++)
        {
            if (mult <= 0)
                break;
            result = 1.2 * result;
            mult--;
        }
        for (int i = 0; i < 20; i++)
        {
            if (mult <= 0)
                break;
            result = 1.05 * result;
            mult--;
        }
        for (; mult > 0; mult--)
        {
            if (mult <= 0)
                break;
            result = 1.01 * result;
            //mult--;
        }
        return (ulong)result;
    }
    public void SetText()
    {
        newLifeText.text = $"{newLifeDes.GetLocalizedString()}\r\n\r\n{soulStone.GetLocalizedString()} : {GetCurNewSoul()}";
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
