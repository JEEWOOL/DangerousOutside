using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    static private CharacterManager _instance;
    public static CharacterManager INSTANCE { get { return _instance; } }
    public CharInfo charInfo;
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            charInfo = new CharInfo();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
