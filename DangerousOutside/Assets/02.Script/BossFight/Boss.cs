using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float hp = 1000;
    public float damage = 100;
    public float bossAttackDelay = 1.5f;
    public bool isBossDead = false;
    int curAttack = 0;
    public IEnumerator BossAttack()
    {
        while (!isBossDead)
        {
            yield return new WaitForSeconds(bossAttackDelay * 2);
            curAttack = Random.Range(0, 3);
            switch (curAttack) {
                case 0:
                    AttackLeft();
                    break;
                case 1:
                    AttackRight();
                    break;
                case 2:
                    AttackCenterSide();
                    break;
            }
        }
    }

    public void AttackLeft()
    {
        BossFightManager.INSTANCE.SetBossAttackColor(true, true, false);
    }
    public void AttackRight()
    {
        BossFightManager.INSTANCE.SetBossAttackColor(false, true, true);
    }
    public void AttackCenterSide()
    {
        BossFightManager.INSTANCE.SetBossAttackColor(true, false, true);

    }

}
