using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    static private SoundManager _instance;
    static public SoundManager Instance { get { return _instance; } }

    public bool isSound;

    public AudioClip clickAudio;
    public AudioClip[] startAudio;
    public AudioClip[] mainAudio;
    public AudioClip attackAudio;

    public AudioSource bgmAudioSource;
    public AudioSource gameSoundAudioSource;
    // ¶§¸°´Ù
    public AudioSource uiSound;

    void Start()
    {
        //PlayerPrefs.SetInt("")
        if (_instance == null)
            _instance = this;

        isSound = true;

        MusicChoice(SceneNum.START);
    }   
    
    void Update()
    {
        
    }

    public void MusicChoice(SceneNum num)
    {
        if(num == SceneNum.START)
        {
            int startNum = Random.Range(0, startAudio.Length);
            bgmAudioSource.clip = startAudio[startNum];
            uiSound.clip = clickAudio;
            bgmAudioSource.Play();
        }
        else if(num == SceneNum.MAIN)
        {
            int mainNum = Random.Range(0, mainAudio.Length);
            bgmAudioSource.clip = mainAudio[mainNum];
            gameSoundAudioSource.clip = attackAudio;
            uiSound.clip = clickAudio;
            bgmAudioSource.Play();
        }
    }
    public void ClickSound()
    {
        uiSound.Play();
    }

    public void SoundGostop(TMP_Text soundBtn)
    {
        if (isSound)
        {
            isSound = false;
            soundBtn.text = "Sound ON";
            AllPause();
        }
        else
        {
            isSound = true;
            soundBtn.text = "Sound OFF";
            
            AllStart();
        }
    }

    public void AllStart()
    {        
        bgmAudioSource.mute = false;
        //uiSound.mute = false;
        gameSoundAudioSource.mute = false;
    }
    public void AllPause()
    {        
        bgmAudioSource.mute = true;
        uiSound.mute = true;
        gameSoundAudioSource.mute = true;
    }

    public void SetSound(byte isSound)
    {
        if(isSound == 0)
        {
            this.isSound = false;
            AllPause();
        }
        else if(isSound == 1)
        {
            this.isSound = true;
            AllStart();
        }
    }
    public void SetText(TMP_Text soundBtn)
    {
        if (isSound)
        {
            soundBtn.text = "Sound OFF";
        }
        else if (isSound)
        {
            soundBtn.text = "Sound ON";
        }
    }
}
public enum SceneNum {
    START,
    MAIN
    
}
