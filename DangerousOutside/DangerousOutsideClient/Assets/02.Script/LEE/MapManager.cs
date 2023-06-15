using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    const int STAGE_SIZE = 10;

    static private MapManager _instance;
    static public MapManager Instance { get { return _instance; } }

    public GameObject BackGround;
    public GameObject Ground;

    public Material[] bgMaterials;
    public float bgScrollSpeed = 0.2f;

    public int curStage = 0;

    public float groundSpeed;

    public Transform[] groundsTransform;

    float leftPosX = 0f;
    float rightPosX = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;

    void Start()
    {
        if (_instance == null)
            _instance = this;

        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        groundsTransform = Ground.GetComponentsInChildren<Transform>();

        leftPosX = -(xScreenHalfSize * 2);
        rightPosX = xScreenHalfSize * 2 * (groundsTransform.Length - 1);
        SetMeterial();
    }

    public void SetMeterial()
    {
        BackGround.GetComponent<MeshRenderer>().material = bgMaterials[GameManager.instance.curStage % 10];
    }
    private void Update()
    {
        Vector2 direction = Vector2.right;
        bgMaterials[GameManager.instance.curStage % 10].mainTextureOffset += direction * bgScrollSpeed * Time.deltaTime * GameManager.instance.bgMoveSpeed;

        for (int i = 1; i < groundsTransform.Length; i++)
        {
            groundsTransform[i].position += new Vector3(-groundSpeed, 0, 0) * Time.deltaTime * GameManager.instance.bgMoveSpeed;

            if (groundsTransform[i].position.x < leftPosX)
            {
                Vector3 nextPos = groundsTransform[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                groundsTransform[i].position = nextPos;
            }
        }
    }
}
