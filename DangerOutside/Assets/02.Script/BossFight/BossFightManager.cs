using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossFightManager : MonoBehaviour
{
    public GameObject bossPref;
    public GameObject charPref;
    public GameObject bossPos;
    public GameObject charPos;

    public Button leftAvoide;
    public Button rightAvoide;
    public Button leftParring;
    public Button rightParring;
    public Button charAttack;

    public Boss curBoss;
    public Character curChar;

    public SpriteRenderer leftBossAttack;
    public SpriteRenderer middleBossAttack;
    public SpriteRenderer rightBossAttack;


    static private BossFightManager _instance;
    static public BossFightManager INSTANCE { get { 
            return _instance;
        } 
    }

    void Start()
    {
        _instance = this;

        CreateBossAndChar();
        SetBossAttackColorBase();
    }

    public void CreateBossAndChar()
    {
        curBoss = Instantiate(bossPref, bossPos.transform.position, bossPos.transform.rotation).GetComponent<Boss>();
        curChar = Instantiate(charPref, charPos.transform.position, charPos.transform.rotation).GetComponent<Character>();
        if(CharacterManager.INSTANCE != null)
            curChar.charInfo = CharacterManager.INSTANCE.charInfo;
        curChar.AddComponent<CharControler>();
        StartCoroutine(curBoss.BossAttack());
    }
    public void BossAttacked()
    {
        curBoss.hp -= curChar.charInfo.damage;
    }
    public void SetBossAttackColorBase()
    {
        leftBossAttack.color = new Color(1, 1, 1, 0.3f);
        middleBossAttack.color = new Color(1, 1, 1, 0.3f);
        rightBossAttack.color = new Color(1, 1, 1, 0.3f);
    }
    public void SetBossAttackColor(bool left, bool middle, bool right)
    {
        if(left)
            leftBossAttack.color = new Color(1, 0, 0, 0.3f);
        if(middle)
            middleBossAttack.color = new Color(1, 0, 0, 0.3f);
        if(right)
            rightBossAttack.color = new Color(1, 0, 0, 0.3f);

        StartCoroutine(BossAttack(left, middle, right));
    }
    public IEnumerator BossAttack(bool left, bool middle, bool right)
    {
        yield return new WaitForSeconds(curBoss.bossAttackDelay);
        SetBossAttackColorBase();
        //yield return new WaitForSeconds(0.1f);
        if (curChar.CheckHit(left, middle, right))
            curChar.GetDamage(curBoss.damage);
    } 
}
