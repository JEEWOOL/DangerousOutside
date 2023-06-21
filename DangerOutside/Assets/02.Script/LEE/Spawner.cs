using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnData[] spawnData;
    public Transform[] spawnPoint;

    public SpawnData curStage;
    public int monsterCount = 0;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        InitSpawnData();
    }

    public void InitSpawnData()
    {
        curStage.speed = 1.7f;
        curStage.health = 70;
        curStage.spriteType = 0;
        curStage.spawnTime = 1;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > curStage.spawnTime && monsterCount < 5)
        {
            monsterCount++;
            timer = 0f;
            Spawn();
        }
    }
    public void SetMonster()
    {
        GameManager.instance.poolManager.Init(0);
        monsterCount = 0;
        double rate = (GameManager.instance.curStage + 1) / 50;
        rate += 1;
        curStage.health = 50 + GameManager.instance.curStage * (rate*1.001) + GameManager.instance.curStage * 20;
        curStage.spriteType = (int)(GameManager.instance.curStage % 2);

        GameManager.instance.bgMoveSpeed = 1;
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(curStage);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public double health;
    public float speed;
}