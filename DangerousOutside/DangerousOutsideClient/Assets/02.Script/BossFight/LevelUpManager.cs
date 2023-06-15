using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public Button damageLevelUp;
    public Button healthLevelUp;
    CharInfo charInfo;
    public GameObject charPref;
    public GameObject charPos;
    void Start()
    {
        charInfo = CharacterManager.INSTANCE.charInfo;
        damageLevelUp.onClick.AddListener(DamageLevelUp);
        healthLevelUp.onClick.AddListener(HealthLevelUp);

        Instantiate(charPref, charPos.transform.position, charPos.transform.rotation);

    }

    void DamageLevelUp()
    {
        charInfo.damage += 10;
    }
    void HealthLevelUp()
    {
        charInfo.hp += 100;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("BossFight");
    }

}
