using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public RuntimeAnimatorController[] animatorCon;
    public float speed;
    public double health;
    public double maxHealth;

    public SpriteRenderer hpBar;

    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        //spriter.flipX = target.position.x < rigid.position.x;
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon"))
        {
            return;
        }

        health -= collision.GetComponent<Weapon>().damage;
        GameManager.instance.ShowDamageText();
        UpdateCurHealth();
        SoundManager.Instance.gameSoundAudioSource.Play();

        if (health > 0)
        {
            // ÇÇ°Ý ÀÌÆåÆ®
        }
        else
        {
            // Á×´Â´Ù

            Dead();
        }
    }

    void Dead()
    {
        GameManager.instance.money += (GameManager.instance.curStage + 1)* (uint)Random.Range(1, 4);
        if(GameManager.instance.money >= 10000)
            PlayACL.Instance.UnlockAchievement(GPGSIds.achievement_10000, (isSuccess) => { Debug.Log(isSuccess); });
        GameManager.instance.killCount++;
        if(Random.Range(1, 11) == 2)
        {
            GameManager.instance.dia += (uint)Random.Range(1, 3);
            UIManager.Instance.ShowDiaCount();
        }

        UIManager.Instance.ShowMoney();

        GameManager.instance.spawner.monsterCount--;

        if (GameManager.instance.killCount >= 20)
        {
            if (GameManager.instance.autoNextStage == true)
            {
                GameManager.instance.NextStage();
            }
            else
            {
                GameManager.instance.NextStageButton.interactable = true;
            }
        }
        gameObject.SetActive(false);
        GameManager.instance.bgMoveSpeed = 1f;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animatorCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        UpdateCurHealth();
    }
    public void UpdateCurHealth()
    {
        if(health < 0) { return; }
        hpBar.transform.localScale = new Vector3(0.9f * (float)((health)/maxHealth), 0.7f, 1);
    }
}