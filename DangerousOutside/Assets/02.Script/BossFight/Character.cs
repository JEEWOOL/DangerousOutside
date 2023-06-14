using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Character : MonoBehaviour
{
    BossCharState state;
    public float attackDelay;
    public float avoidDelay;
    public float parringDelay;
    public CharInfo charInfo;
    // Start is called before the first frame update
    void Start()
    {
        state = BossCharState.Idle;
    }

    public bool TryAttack()
    {
        if(state == BossCharState.Idle)
        {
            state = BossCharState.Attack;
            return true;
        }
        return false;
    }
    public IEnumerator Attack(Action action)
    {
        yield return new WaitForSeconds(attackDelay);
        if(state == BossCharState.Attack)
        {
            action();
            state = BossCharState.Idle;
            yield break;
        }
        yield break;
    }
    public bool TryLeftAvoid()
    {
        if (state == BossCharState.Idle)
        {
            state = BossCharState.LeftAvoid;
            return true;
        }
        return false;
    }
    public bool TryRightAvoid()
    {
        if (state == BossCharState.Idle)
        {
            state = BossCharState.RightAvoid;
            return true;
        }
        return false;
    }
    public bool TryLeftParring()
    {
        if (state == BossCharState.Idle)
        {
            state = BossCharState.LeftParring;
            return true;
        }
        return false;
    }
    public bool TryRightParring()
    {
        if (state == BossCharState.Idle)
        {
            state = BossCharState.RightParring;
            return true;
        }
        return false;
    }
    public IEnumerator SetDelayIdle(BossCharState exState, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (state == exState)
        {
            state = BossCharState.Idle;
            this.transform.position = new Vector3(0, -2, 0);
        }
    }
    public bool CheckHit(bool left, bool middle, bool right)
    {
        if (state == BossCharState.Attack)
            return true;
        if (left && state == BossCharState.LeftAvoid)
            return true;
        if (middle && state == BossCharState.Idle)
            return true;
        if (right && state == BossCharState.RightAvoid)
            return true;

        return false;
    }

    public void GetDamage(float bossDamage)
    {
        charInfo.hp -= bossDamage;
        Debug.Log($"{charInfo.hp}");
        if(charInfo.hp <= 0)
        {
            //TODO: »ç¸Á
        }
        state = BossCharState.Damage;
        StartCoroutine(SetDelayIdle(BossCharState.Damage, 0.2f));
    }
}
