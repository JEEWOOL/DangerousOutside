using UnityEngine;
using System;
using System.Collections;
//gpg
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
//for encoding
using System.Text;
//for extra save ui
using UnityEngine.SocialPlatforms;
//for text, remove
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayCloudDataManager : MonoBehaviour
{

    private static PlayCloudDataManager instance;
    public SaveDatas loadedDatas;
    public Button StartButton;

    private bool isLoging = false;
    public static PlayCloudDataManager Instance
    {
        get
        {
            return instance;
        }
    }

    public bool isProcessing
    {
        get;
        private set;
    }
    private const string m_saveFileName = "game_save_data";
    public bool isAuthenticated
    {
        get
        {
            return Social.localUser.authenticated;
        }
    }

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        loadedDatas = new SaveDatas(0);
        if (instance == null)
        {
            instance = this;
        }
        InitiatePlayGames();
        Login();
    }
    private void InitiatePlayGames()
    {

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
            .Builder()
            .RequestServerAuthCode(false)
            .RequestEmail() // 이메일 권한을 얻고 싶지 않다면 해당 줄(RequestEmail)을 지워주세요.
            .RequestIdToken()
            .EnableSavedGames()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true; // 디버그 로그를 보고 싶지 않다면 false로 바꿔주세요.
                                                  //GPGS 시작.
        PlayGamesPlatform.Activate();
    }


    public void Login()
    {
        // 이미 로그인 된 경우
        if (PlayGamesPlatform.Instance.localUser.authenticated == true)
        {
            LoadCurState();
            if (StartButton != null)
                Invoke("StartButtonEnable", 0);
        }
        else
        {
            if (isLoging == true)
                return;
            isLoging = true;
            PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
            {
                if (!success)
                {
                    Debug.Log("Fail Login");
                }
                else
                {
                    LoadCurState();
                }
            });
        }
    }
    public void LogOut()
    {
        if (Social.localUser.authenticated == false)
            return;
        PlayGamesPlatform.Instance.SignOut();
    }
    private static void ChangeToLEE()
    {
        //if (SceneManager.GetActiveScene().name != "LEE")
        //{
        //    SceneManager.LoadScene("LEE");
        //}
    }

    public void SaveCurState()
    {
        if (GameManager.instance == null)
        {
            return;
        }
        if (isProcessing)
        {
            return;
        }
        SaveDatas();
    }

    //public static void SaveDatas()
    //{
    //    SaveDatas datas = new SaveDatas();
    //    datas.curStage = GameManager.instance.curStage;
    //    datas.highstStage = GameManager.instance.highstStage;
    //    datas.money = GameManager.instance.money;
    //    datas.dia = GameManager.instance.dia;
    //    datas.soul = NewLifeManager.Instance.soul;
    //    datas.goldPowerLv = CharStateManager.Instance.goldPowerLv;
    //    datas.soulPowerLv = CharStateManager.Instance.soulPowerLv;
    //    datas.weaponSkillLv = CharStateManager.Instance.weaponSkillLv;
    //    byte[] datass = global::SaveDatas.StructToBytes(datas);
    //    for(int i = 0;i< datass.Length;i++)
    //        Debug.Log(datass[i]);
    //    PlayCloudDataManager.Instance.SaveToCloud(datass);
    //}

    public void SaveDatas()
    {
        int blen = 0;
        blen += sizeof(Int64);
        blen += sizeof(Int64);
        blen += sizeof(Int64);
        blen += sizeof(Int64);

        blen += sizeof(Int64);
        blen += sizeof(Int64);
        blen += sizeof(Int64);
        blen += sizeof(Int64);

        blen += sizeof(byte);

        blen += sizeof(int);
        blen += sizeof(int);
        blen += sizeof(int);

        blen += sizeof(int) * CostumeManager.Instance.haveWeapon.Count;
        blen += sizeof(int) * CostumeManager.Instance.haveShield.Count;
        blen += sizeof(int) * CostumeManager.Instance.haveHelmet.Count;

        blen += sizeof(int) * GameManager.instance.playerEquips.Length;

        PacketResolver packet = new PacketResolver(blen);
        packet.push(BitConverter.GetBytes(GameManager.instance.curStage));
        packet.push(BitConverter.GetBytes(GameManager.instance.highstStage));
        packet.push(BitConverter.GetBytes(GameManager.instance.money));
        packet.push(BitConverter.GetBytes(GameManager.instance.dia));

        packet.push(BitConverter.GetBytes(NewLifeManager.Instance.soul));
        packet.push(BitConverter.GetBytes(CharStateManager.Instance.goldPowerLv));
        packet.push(BitConverter.GetBytes(CharStateManager.Instance.soulPowerLv));
        packet.push(BitConverter.GetBytes(CharStateManager.Instance.weaponSkillLv));

        packet.push(BitConverter.GetBytes(SoundManager.Instance.isSound));

        packet.push(CostumeManager.Instance.haveWeapon.Count);
        for (int i = 0; i < CostumeManager.Instance.haveWeapon.Count; i++)
        {
            packet.push(CostumeManager.Instance.haveWeapon[i]);
        }
        packet.push(CostumeManager.Instance.haveShield.Count);
        for (int i = 0; i < CostumeManager.Instance.haveShield.Count; i++)
        {
            packet.push(CostumeManager.Instance.haveShield[i]);
        }
        packet.push(CostumeManager.Instance.haveHelmet.Count);
        for (int i = 0; i < CostumeManager.Instance.haveHelmet.Count; i++)
        {
            packet.push(CostumeManager.Instance.haveHelmet[i]);
        }

        for (int i = 0; i < GameManager.instance.playerEquips.Length; i++)
        {
            packet.push(GameManager.instance.playerEquips[i]);
        }
        PlayCloudDataManager.Instance.SaveToCloud(packet.buffer);
    }

    public void LoadCurState()
    {
        LoadFromCloud((byte[] result) =>
        {
            if (result.Length == 0)
            {
                if (StartButton != null)
                    Invoke("StartButtonEnable", 0);
                return;
            }
            PacketResolver packet = new PacketResolver(result);

            loadedDatas.curStage = (ulong)packet.pop_int64();
            loadedDatas.highstStage = (ulong)packet.pop_int64();
            loadedDatas.money = (ulong)packet.pop_int64();
            loadedDatas.dia = (ulong)packet.pop_int64();
            loadedDatas.soul = (ulong)packet.pop_int64();
            loadedDatas.goldPowerLv = (ulong)packet.pop_int64();
            loadedDatas.soulPowerLv = (ulong)packet.pop_int64();
            loadedDatas.weaponSkillLv = (ulong)packet.pop_int64();

            loadedDatas.isSound = packet.pop_byte();
            int count = packet.pop_int32();
            for (int i = 0; i < count; i++)
            {
                loadedDatas.haveWeapon.Add(packet.pop_int32());
            }
            count = packet.pop_int32();
            for (int i = 0; i < count; i++)
            {
                loadedDatas.haveShield.Add(packet.pop_int32());
            }
            count = packet.pop_int32();
            for (int i = 0; i < count; i++)
            {
                loadedDatas.haveHelmet.Add(packet.pop_int32());
            }

            for (int i = 0; i < 3; i++)
            {
                loadedDatas.playerEquip[i] = packet.pop_int32();
            }
            if (StartButton != null)
                Invoke("StartButtonEnable", 0);
            isLoging = false;
            if (GameManager.instance == null)
                return;
            SetCurToLoaded();
        });
    }
    public void StartButtonEnable()
    {
        StartButton.interactable = enabled;
    }
    public void SetCurToLoaded()
    {
        GameManager.instance.curStage = loadedDatas.curStage;
        GameManager.instance.highstStage = loadedDatas.highstStage;
        GameManager.instance.money = loadedDatas.money;
        GameManager.instance.dia = loadedDatas.dia;
        NewLifeManager.Instance.soul = loadedDatas.soul;
        CharStateManager.Instance.goldPowerLv = loadedDatas.goldPowerLv;
        CharStateManager.Instance.soulPowerLv = loadedDatas.soulPowerLv;
        CharStateManager.Instance.weaponSkillLv = loadedDatas.weaponSkillLv;


        CostumeManager.Instance.haveWeapon = loadedDatas.haveWeapon;
        CostumeManager.Instance.haveShield = loadedDatas.haveShield;
        CostumeManager.Instance.haveHelmet = loadedDatas.haveHelmet;

        if (!CostumeManager.Instance.haveWeapon.Contains(0))
            CostumeManager.Instance.haveWeapon.Add(0);
        if (!CostumeManager.Instance.haveShield.Contains(0))
            CostumeManager.Instance.haveShield.Add(0);
        if (!CostumeManager.Instance.haveHelmet.Contains(0))
            CostumeManager.Instance.haveHelmet.Add(0);

        GameManager.instance.playerEquips = loadedDatas.playerEquip;

        UIManager.Instance.ShowMoney();
        UIManager.Instance.ShowSoul();
        UIManager.Instance.ShowDiaCount();
        UIManager.Instance.ShowPowerUpMoney();
        UIManager.Instance.ShowPowerUpSoul();
        UIManager.Instance.ShowWeaponSkill();
        GameManager.instance.SetDamage();
        GameManager.instance.InitState();

        SoundManager.Instance.SetSound(loadedDatas.isSound);
        CostumeManager.Instance.SetHavingEquip();
        CostumeManager.Instance.SetEquip();
    }

    public void LoadFromCloud(Action<byte[]> afterLoadAction)
    {
        if (isAuthenticated && !isProcessing)
        {
            StartCoroutine(LoadFromCloudRoutin(afterLoadAction));
        }
        else
        {
            Login();
            LoadCurState();
        }
    }

    private IEnumerator LoadFromCloudRoutin(Action<byte[]> loadAction)
    {
        byte[] result = null;
        isProcessing = true;
        Debug.Log("Loading game progress from the cloud.");

        PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(
            m_saveFileName, //name of file.
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            (SavedGameRequestStatus status, ISavedGameMetadata metaData) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(
                        metaData, (SavedGameRequestStatus status, byte[] bytes) =>
                    {
                        if (status != SavedGameRequestStatus.Success)
                        {
                            Debug.LogWarning("Error Saving" + status);
                        }
                        else
                        {
                            if (bytes == null)
                            {
                                Debug.Log("No Data saved to the cloud");
                                return;
                            }
                            result = bytes;
                        }
                        isProcessing = false;
                    });
                }
                else
                {
                    Debug.LogWarning("Error opening Saved Game" + status);
                }
            });

        while (isProcessing)
        {
            yield return null;
        }

        loadAction.Invoke(result);
    }

    public void SaveToCloud(byte[] dataToSave)
    {
        if (isAuthenticated)
        {
            isProcessing = true;
            PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(
                m_saveFileName, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                (SavedGameRequestStatus status, ISavedGameMetadata metaData) =>
                {
                    if (status == SavedGameRequestStatus.Success)
                    {
                        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
                            .WithUpdatedPlayedTime(DateTime.Now.TimeOfDay);
                        //.WithUpdatedDescription("Saved game at " + DateTime.Now);

                        SavedGameMetadataUpdate updatedMetadata = builder.Build();

                        PlayGamesPlatform.Instance.SavedGame.CommitUpdate(metaData, updatedMetadata, dataToSave, OnGameSave);
                    }
                    else
                    {
                        Debug.LogWarning("Error opening Saved Game" + status);
                    }
                });
        }
        else
        {
            Login();
        }
    }
    public void DeleteCloud(Action<bool> onCloudDeleted = null)
    {
        PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(m_saveFileName,
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    PlayGamesPlatform.Instance.SavedGame.Delete(game);
                    onCloudDeleted?.Invoke(true);
                }
                else
                    onCloudDeleted?.Invoke(false);
            });
    }
    private void OnGameSave(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if (status != SavedGameRequestStatus.Success)
        {
            Debug.LogWarning("Error Saving" + status);
        }

        isProcessing = false;
    }
    private byte[] StringToBytes(string stringToConvert)
    {
        return Encoding.UTF8.GetBytes(stringToConvert);
    }

    private string BytesToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }
}