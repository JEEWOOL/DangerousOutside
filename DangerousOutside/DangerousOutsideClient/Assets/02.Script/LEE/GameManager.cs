using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float bgMoveSpeed;
    public PoolManager poolManager;
    public Player player;
    public Spawner spawner;
    public Weapon weapon;

    public int[] playerEquips = new int[3];

    public Button NextStageButton; 

    public string NickName = "���";

    public ulong curStage = 0;
    public ulong highstStage = 0;
    public int killCount = 0;

    public ulong money = 0;
    public ulong dia = 0;

    public bool autoNextStage = false;

    public GameObject damageTextPref;
    public Transform damageTextPlace;

    private void Awake()
    {
        instance = this;
        NickName = PlayGamesPlatform.Instance.GetUserDisplayName();

        SoundManager.Instance.MusicChoice(SceneNum.MAIN);
    }
    public void SetAutoNextStage(Toggle toggle)
    {
        autoNextStage = toggle.isOn;

        if (toggle.isOn)
        {
            UIManager.Instance.autoText.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            UIManager.Instance.autoText.color = Color.white;
        }
    }
    public void SetDamage()
    {
        ulong curDamage = 5 + CharStateManager.Instance.goldPowerLv * 5;
            //+ (ulong)((EnhanceManager.Instance.curWeapon.rate+1) * 20 + EnhanceManager.Instance.curWeaponEnhance * 2);
        curDamage = (ulong)(curDamage * ((CharStateManager.Instance.soulPowerLv + 100) / 100.0));
        curDamage = (ulong)(curDamage * ((CharStateManager.Instance.weaponSkillLv + 100) / 100.0));
        weapon.damage = curDamage;
        //UIManager.Instance.ShowDamage();
    }
    public void SetHealth()
    {
        ulong curHealth = CharStateManager.Instance.goldMaxHealth + (ulong)((EnhanceManager.Instance.curSheild.rate + 1) * 50 + EnhanceManager.Instance.curSheildEnhance * 4);
        curHealth = (ulong)(curHealth * ((CharStateManager.Instance.soulMaxHealth + 100) / 100));
        player.maxHealth = curHealth;
        //UIManager.Instance.ShowDamage();
    }
    public void NextStage()
    {
        curStage++;
        if(curStage > highstStage)
        {
            highstStage = curStage;
            PlayACL.Instance.ReportLeaderboard(GPGSIds.leaderboard, (long)highstStage + 1);
            switch (highstStage + 1) {
                case 20:
                PlayACL.Instance.UnlockAchievement(GPGSIds.achievement_20, (isSuccess) => { Debug.Log(isSuccess); });
                break;
                case 50:
                PlayACL.Instance.UnlockAchievement(GPGSIds.achievement_50, (isSuccess) => { Debug.Log(isSuccess); });
                break;
                case 100:
                PlayACL.Instance.UnlockAchievement(GPGSIds.achievement_100, (isSuccess) => { Debug.Log(isSuccess); });
                break;
                case 500:
                PlayACL.Instance.UnlockAchievement(GPGSIds.achievement_500, (isSuccess) => { Debug.Log(isSuccess); });
                break;
                case 1000:
                PlayACL.Instance.UnlockAchievement(GPGSIds.achievement_1000, (isSuccess) => { Debug.Log(isSuccess); });
                break;
                case 10000:
                PlayACL.Instance.UnlockAchievement(GPGSIds.achievement_10000, (isSuccess) => { Debug.Log(isSuccess); });
                break;

            }

            //if (highstStage == 20)
            //{
            //    PlayACL.Instance.UnlockAchievement(GPGSIds.achievement_20, (isSuccess) => { Debug.Log(isSuccess); });
            //}
        }
        killCount = 0;
        NextStageButton.interactable = false;
        spawner.SetMonster();
        MapManager.Instance.SetMeterial();
        UIManager.Instance.ShowStage();
    }
    public void InitState()
    {
        killCount = 0;
        NextStageButton.interactable = false;
        spawner.SetMonster();
        MapManager.Instance.SetMeterial();
        UIManager.Instance.ShowStage();
    }
    public void LoadMoney()
    {
        PlayCloudDataManager.Instance.LoadCurState();
    }
    public void SaveMoney()
    {
        PlayCloudDataManager.Instance.SaveCurState();
    }
    public void ShowLeaderBoardUI()
    {
        PlayACL.Instance.ShowTargetLeaderboardUI(GPGSIds.leaderboard);
    }
    public void ShowAchieveUI()
    {
        PlayACL.Instance.ShowAchievementUI();
    }
    public void DeleteCloude()
    {
        PlayCloudDataManager.Instance.DeleteCloud();
    }
    public void ShowDamageText()
    {
        GameObject tempText = Instantiate(damageTextPref, damageTextPlace);
        Destroy(tempText, 2f);
    }
}
