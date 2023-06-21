using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharStateManager : MonoBehaviour
{

    public GameObject goldPannel;
    public GameObject soulPannel;

    public ulong healthUpMoney = 10;

    public ulong goldPowerLv = 0;
    public int goldHealthLv = 0;
    public ulong weaponSkillLv = 0;

    public ulong goldMaxHealth = 100;

    public ulong healthUpSoul = 1;

    public ulong soulPowerLv = 0;
    public int soulHealthLv = 0;

    public float soulMaxHealth = 0;


    static private CharStateManager _instance;
    static public CharStateManager Instance { get { return _instance; } }

    void Start()
    {
        if (_instance == null)
            _instance = this;
        GameManager.instance.SetDamage();
    }
    public void ChangePannel(int num)
    {
        if (num != 0)
        {
            goldPannel.SetActive(false);
        }
        if (num == 0)
        {
            goldPannel.SetActive(true);
        }

        if (num != 1)
        {
            soulPannel.SetActive(false);
        }
        if (num == 1)
        {
            soulPannel.SetActive(true);
        }
    }

    public void GoldPowerUp()
    {
        ulong cost = (ulong)(goldPowerLv + 1) * 10;
        if (GameManager.instance.money >= cost)
        {
            GameManager.instance.money -= cost;
            goldPowerLv++;

            UIManager.Instance.ShowPowerUpMoney();
            UIManager.Instance.ShowMoney();
            GameManager.instance.SetDamage();
        }
    }
    public void SoulPowerUp()
    {
        if (NewLifeManager.Instance.soul >= soulPowerLv + 1)
        {
            NewLifeManager.Instance.soul -= soulPowerLv + 1;
            soulPowerLv++;

            UIManager.Instance.ShowPowerUpSoul();
            UIManager.Instance.ShowSoul();
            GameManager.instance.SetDamage();

        }
    }
    public void WeaponSkillUp()
    {
        ulong cost = (ulong)(weaponSkillLv + 1);
        if (GameManager.instance.dia >= cost)
        {
            GameManager.instance.dia -= cost;
            weaponSkillLv += 1;

            if(weaponSkillLv == 100)
                PlayACL.Instance.UnlockAchievement(GPGSIds.achievement__100lv, (isSuccess) => { Debug.Log(isSuccess); });

            UIManager.Instance.ShowWeaponSkill();
            UIManager.Instance.ShowDiaCount();
            GameManager.instance.SetDamage();
        }
    }

    public void SoulMaxHealthUp()
    {
        if (NewLifeManager.Instance.soul >= healthUpSoul)
        {
            NewLifeManager.Instance.soul -= healthUpSoul;
            healthUpSoul += 1;
            soulHealthLv++;

            UIManager.Instance.ShowSoul();

            soulMaxHealth += 1;
        }
    }

}
