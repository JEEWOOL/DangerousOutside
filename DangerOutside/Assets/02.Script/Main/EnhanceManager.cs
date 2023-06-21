using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceManager : MonoBehaviour
{
    public EquipmentAsset[] weaponDetails;
    public EquipmentAsset[] sheildDetails;

    public EquipmentAsset curWeapon;
    public EquipmentAsset curSheild;

    public int curWeaponEnhance;
    public int curSheildEnhance;

    public TMP_Text weaponPrice;
    public TMP_Text sheildPrice;
    public TMP_Text weaponUpgradeCount;
    public TMP_Text sheildUpgradeCount;

    public Button weaponRateUpButton;
    public Button sheildRateUpButton;

    public GameObject weaponEnhanceButton;
    public GameObject sheildEnhanceButton;

    public Image weaponImage;
    public Image sheildImage;

    static private EnhanceManager _instance;
    static public EnhanceManager Instance { get { return _instance; } }

    void Start()
    {
        if (_instance == null)
            _instance = this;

        weaponEnhanceButton.GetComponent<Button>().onClick.AddListener(WeaponEnhance);
        sheildEnhanceButton.GetComponent<Button>().onClick.AddListener(SheildEnhance);

        weaponRateUpButton.onClick.AddListener(WeaponRateUp);
        sheildRateUpButton.onClick.AddListener(SheildRateUp);
        Init();
    }
    void Init()
    {
        curWeapon = weaponDetails[0];
        curSheild = sheildDetails[0];
        curWeaponEnhance = 1;
        curSheildEnhance = 1;
        SetWeaponPrice();
        SetSheildPrice();
    }
    void SetWeaponPrice()
    {
        weaponPrice.text = $"{curWeapon.price + curWeaponEnhance * (curWeapon.price / 10)}";
        weaponUpgradeCount.text = $"{curWeaponEnhance}/10";
        weaponEnhanceButton.GetComponent<Image>().color = curWeapon.color;
        weaponImage.sprite = curWeapon.sprite;
        weaponImage.rectTransform.localScale = new Vector3(0.6f * curWeapon.sprite.rect.width / 7,
            0.6f * curWeapon.sprite.rect.height / 18, 1);
        GameManager.instance.player.weapon.GetComponent<SpriteRenderer>().sprite = curWeapon.sprite;
    }
    void SetSheildPrice()
    {
        sheildPrice.text = $"{curSheild.price * curSheildEnhance * (curSheild.price / 10)}";
        sheildUpgradeCount.text = $"{curSheildEnhance}/10";
        sheildEnhanceButton.GetComponent<Image>().color = curSheild.color;
        sheildImage.sprite = curSheild.sprite;
        sheildImage.rectTransform.localScale = new Vector3(0.6f * curSheild.sprite.rect.width / 14,
            0.6f * curSheild.sprite.rect.height / 14, 1);
        GameManager.instance.player.shield.GetComponent<SpriteRenderer>().sprite = curSheild.sprite;
    }

    void WeaponEnhance()
    {
        if (curWeaponEnhance == 10 || GameManager.instance.dia < (ulong)(curWeapon.price + curWeaponEnhance*(curWeapon.price/10)))
            return;
        GameManager.instance.dia -= (ulong)(curWeapon.price + curWeaponEnhance * (curWeapon.price / 10));
        curWeaponEnhance++;
        SetWeaponPrice();
    }
    void SheildEnhance()
    {
        if (curSheildEnhance == 10 || GameManager.instance.dia < (ulong)(curSheild.price * curSheildEnhance * (curSheild.price / 10)))
            return;
        GameManager.instance.dia -= (ulong)(curSheild.price * curSheildEnhance * (curSheild.price / 10));
        curSheildEnhance++;
        SetSheildPrice();
    }
    void WeaponRateUp()
    {
        if (curWeaponEnhance != 10 || GameManager.instance.dia < (ulong)(curWeapon.price + curWeaponEnhance * (curWeapon.price / 10))
                    || NewLifeManager.Instance.soul < (ulong)(curWeapon.rate * 50))
            return;
        curWeapon = weaponDetails[curWeapon.rate + 1];
        NewLifeManager.Instance.soul -= (ulong)curWeapon.rate * 50;
        curWeaponEnhance = 1;
        SetWeaponPrice();
    }
    void SheildRateUp()
    {
        if (curSheildEnhance != 10 || GameManager.instance.dia < (ulong)(curSheild.price * curSheildEnhance * (curSheild.price / 10))
                    || NewLifeManager.Instance.soul < (ulong)curSheild.rate * 50)
            return;
        curSheild = sheildDetails[curSheild.rate + 1];
        NewLifeManager.Instance.soul -= (ulong)curSheild.rate * 50;
        curSheildEnhance = 1;
        SetSheildPrice();
    }
}
