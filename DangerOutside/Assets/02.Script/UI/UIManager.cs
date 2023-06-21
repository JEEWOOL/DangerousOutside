using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class UIManager : MonoBehaviour
{
    public TMP_Text NickName;
    public TMP_Text Money;
    public TMP_Text Diamond;
    public TMP_Text Soul;

    public TMP_Text powerUpMoney;
    public TMP_Text gold_curPowerNextPower;
    public TMP_Text gold_powerLv;

    public TMP_Text powerUpSoul;
    public TMP_Text soul_curPowerNextPower;
    public TMP_Text soul_powerLv;

    public TMP_Text weaponCost;
    public TMP_Text weapon_curPowerNextPower;
    public TMP_Text weapon_powerLv;

    public TMP_Text curDamage;

    public TMP_Text diaCount;

    public TMP_Text autoText;

    public TMP_Text stageText;

    public TMP_Text addsGoldText;
    public TMP_Text purchaseGoldText;

    private LocalizedString lStage;
    private LocalizedString lHighStage;
    private LocalizedString lAds;
    private LocalizedString lPurchase;

    static private UIManager _instance;
    static public UIManager Instance { get { return _instance; } }

    void Start()
    {
        if(_instance == null)
            _instance = this;
        ShowNickName(GameManager.instance.NickName);

        lStage = new LocalizedString("INGAME", "Stage");
        lHighStage = new LocalizedString("INGAME", "Highest");
        lAds = new LocalizedString("UITAB", "AdsGold");
        lPurchase = new LocalizedString("UITAB", "PurchaseGold");
    }

    public void ShowNickName(string nickName)
    {
        NickName.text = nickName;
    }
    public void ShowMoney()
    {
        Money.text = string.Format("{0:#,0}", GameManager.instance.money);
        PlayCloudDataManager.Instance.SaveCurState();
    }
    public void ShowNewLifeStone()
    {
        Soul.text = string.Format("{0:#,0}", NewLifeManager.Instance.soul);
    }
    public void ShowPowerUpMoney()
    {
        powerUpMoney.text = string.Format("{0:#,0}", (CharStateManager.Instance.goldPowerLv + 1) * 10);
        gold_curPowerNextPower.text = string.Format("{0:#,0}", $"{(ulong)CharStateManager.Instance.goldPowerLv * 5 + 5} => " +
            $"{(ulong)CharStateManager.Instance.goldPowerLv * 5 + 10}");
        gold_powerLv.text = string.Format("{0:#,0}", $"Lv.{CharStateManager.Instance.goldPowerLv}");
    }
    public void ShowSoul()
    {
        Soul.text = string.Format("{0:#,0}", NewLifeManager.Instance.soul);
        PlayCloudDataManager.Instance.SaveCurState();
    }
    public void ShowPowerUpSoul()
    {
        powerUpSoul.text = string.Format("{0:#,0}", CharStateManager.Instance.soulPowerLv + 1);
        soul_curPowerNextPower.text = string.Format("{0:#,0}", $"{CharStateManager.Instance.soulPowerLv}% => " +
            $"{CharStateManager.Instance.soulPowerLv + 1}%");
        soul_powerLv.text = string.Format("{0:#,0}", $"Lv.{CharStateManager.Instance.soulPowerLv}");
    }
    public void ShowWeaponSkill()
    {
        weaponCost.text = string.Format("{0:#,0}", (CharStateManager.Instance.weaponSkillLv + 1));
        weapon_curPowerNextPower.text = string.Format("{0:#,0}", $"{CharStateManager.Instance.weaponSkillLv}% => " +
            $"{CharStateManager.Instance.weaponSkillLv + 1}%");
        weapon_powerLv.text = string.Format("{0:#,0}", $"Lv.{CharStateManager.Instance.weaponSkillLv}");
    }
    public void ShowDamage()
    {
        curDamage.text = $"{GameManager.instance.weapon.damage}";
    }
    public void ShowDiaCount()
    {
        diaCount.text = string.Format("{0:#,0}", GameManager.instance.dia);
        PlayCloudDataManager.Instance.SaveCurState();
    }
    public void ShowStage()
    {
        stageText.text = $"{lStage.GetLocalizedString()} : {GameManager.instance.curStage + 1}\n{lHighStage.GetLocalizedString()} : {GameManager.instance.highstStage + 1}";
    }
    public void ShowStoreText()
    {
         addsGoldText.text = $"{lAds.GetLocalizedString()}\r\n<sprite=0>" + string.Format("{0:#,0}", 200 * (GameManager.instance.highstStage+1)); 
         purchaseGoldText.text = $"{lPurchase.GetLocalizedString()}\r\n<sprite=0>" + string.Format("{0:#,0}", 2000 * (GameManager.instance.highstStage+1)); 
    }
}
 