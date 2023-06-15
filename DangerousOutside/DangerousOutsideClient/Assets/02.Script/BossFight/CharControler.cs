using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharControler : MonoBehaviour
{
    public Character curChar;

    public Button leftAvoide;
    public Button rightAvoide;
    public Button leftParring;
    public Button rightParring;
    public Button charAttack;

    private Vector3 Left = new Vector3(-1.5f, -2, 0);
    private Vector3 Middle = new Vector3(0, -2, 0);
    private Vector3 Right = new Vector3(1.5f, -2, 0);

    void Start()
    {
        curChar = GetComponent<Character>();
        leftAvoide = BossFightManager.INSTANCE.leftAvoide;
        rightAvoide = BossFightManager.INSTANCE.rightAvoide;
        leftParring = BossFightManager.INSTANCE.leftParring;
        rightParring = BossFightManager.INSTANCE.rightParring;
        charAttack = BossFightManager.INSTANCE.charAttack;
        charAttack.onClick.AddListener(AttackClick);
        leftAvoide.onClick.AddListener(LeftAvoidClick);
        rightAvoide.onClick.AddListener(RightAvoidClick);
    }
    void AttackClick()
    {
        if (curChar.TryAttack())
        {
            StartCoroutine(curChar.Attack(BossFightManager.INSTANCE.BossAttacked));
        }
    }
    void LeftAvoidClick()
    {
        if (curChar.TryLeftAvoid())
        {
            this.transform.position = Left;
            StartCoroutine(curChar.SetDelayIdle(BossCharState.LeftAvoid, 0.5f));
        }
    }
    void RightAvoidClick()
    {
        if (curChar.TryRightAvoid())
        {
            this.transform.position = Right;
            StartCoroutine(curChar.SetDelayIdle(BossCharState.RightAvoid, 0.5f));
        }
    }

}
