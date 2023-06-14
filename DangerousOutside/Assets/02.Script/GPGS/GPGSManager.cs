using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.CompositeCollider2D;

public class GPGSManager : MonoBehaviour
{
    //static private GPGSManager instance;

    //public static GPGSManager Instance
    //{
    //    get
    //    {
    //        if(instance == null)
    //        {
    //            instance = FindObjectOfType<GPGSManager>();

    //            if(instance == null)
    //            {
    //                instance = new GameObject("Google Play Service").AddComponent<GPGSManager>();
    //            }
    //        }

    //        return instance;
    //    }
    //}

    void Awake()
    {
       //GPGS �÷����� ����
       PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
           .Builder()
           .RequestServerAuthCode(false)
           .RequestEmail() // �̸��� ������ ��� ���� �ʴٸ� �ش� ��(RequestEmail)�� �����ּ���.
           .RequestIdToken()
           .EnableSavedGames()
           .Build();
        //Ŀ���� �� ������ GPGS �ʱ�ȭ
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true; // ����� �α׸� ���� ���� �ʴٸ� false�� �ٲ��ּ���.
                                                  //GPGS ����.
        PlayGamesPlatform.Activate();

        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
        //    .Builder()
        //    .RequestServerAuthCode(false)
        //    .RequestEmail() // �̸��� ������ ��� ���� �ʴٸ� �ش� ��(RequestEmail)�� �����ּ���.
        //    .RequestIdToken()
        //    .EnableSavedGames()
        //    .Build();
        //PlayGamesPlatform.Activate();
    }

    //public void Login()
    //{
    //    Social.localUser.Authenticate((bool success) => { if (!success) { Debug.Log("�α��� ����"); } });
    //}

    public void GPGSLogin()
    {
        // �̹� �α��� �� ���
        if (Social.localUser.authenticated == true)
        {
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                }
                else
                {
                    // �α��� ����
                    Debug.Log("Login failed for some reason");
                }
            });
        }
    }

    // ���� ��ū �޾ƿ�
    public string GetTokens()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // ���� ��ū �ޱ� ù ��° ���
            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            // �� ��° ���
            // string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
            return _IDtoken;
        }
        else
        {
            Debug.Log("���ӵǾ� ���� �ʽ��ϴ�. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
            return null;
        }
    }
    public void LogOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }
}
