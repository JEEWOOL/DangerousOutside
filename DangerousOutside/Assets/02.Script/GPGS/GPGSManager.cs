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
       //GPGS 플러그인 설정
       PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
           .Builder()
           .RequestServerAuthCode(false)
           .RequestEmail() // 이메일 권한을 얻고 싶지 않다면 해당 줄(RequestEmail)을 지워주세요.
           .RequestIdToken()
           .EnableSavedGames()
           .Build();
        //커스텀 된 정보로 GPGS 초기화
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true; // 디버그 로그를 보고 싶지 않다면 false로 바꿔주세요.
                                                  //GPGS 시작.
        PlayGamesPlatform.Activate();

        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
        //    .Builder()
        //    .RequestServerAuthCode(false)
        //    .RequestEmail() // 이메일 권한을 얻고 싶지 않다면 해당 줄(RequestEmail)을 지워주세요.
        //    .RequestIdToken()
        //    .EnableSavedGames()
        //    .Build();
        //PlayGamesPlatform.Activate();
    }

    //public void Login()
    //{
    //    Social.localUser.Authenticate((bool success) => { if (!success) { Debug.Log("로그인 실패"); } });
    //}

    public void GPGSLogin()
    {
        // 이미 로그인 된 경우
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
                    // 로그인 실패
                    Debug.Log("Login failed for some reason");
                }
            });
        }
    }

    // 구글 토큰 받아옴
    public string GetTokens()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // 유저 토큰 받기 첫 번째 방법
            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            // 두 번째 방법
            // string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
            return _IDtoken;
        }
        else
        {
            Debug.Log("접속되어 있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
            return null;
        }
    }
    public void LogOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }
}
