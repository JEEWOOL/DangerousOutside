using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    public float health;
    public float maxHealth = 10;

    public GameObject weapon;
    public GameObject shield;
    public GameObject helmet;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    private void Update()
    {
        if (GameManager.instance.bgMoveSpeed == 0)
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        GameManager.instance.bgMoveSpeed = 0;
    }
    public void AttackCollStart()
    {
        GameManager.instance.weapon.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void AttackCollStop()
    {
        GameManager.instance.weapon.GetComponent<BoxCollider2D>().enabled = false;
    }
}
