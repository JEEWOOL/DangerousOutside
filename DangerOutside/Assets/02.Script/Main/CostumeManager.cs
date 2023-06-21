using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class CostumeManager : MonoBehaviour
{
    public List<int> haveWeapon = new List<int>();
    public List<int> haveShield = new List<int>();
    public List<int> haveHelmet = new List<int>();

    public CostumeAsset[] weaponCos;
    public CostumeAsset[] shieldCos;
    public CostumeAsset[] helmetCos;

    public Transform weaponParent;
    public Transform shieldParent;
    public Transform helmetParent;
    public GameObject go_Item;
    public GameObject checkPanel;
    public GameObject equipPanel;

    public Button confirmBtn;
    public Button yesButton;
    public Sprite changeImage;

    private LocalizedString lPurchased;

    static private CostumeManager _instance;
    static public CostumeManager Instance { get { return _instance; } }

    private void Start()
    {
        if (_instance == null)
            _instance = this;

        lPurchased = new LocalizedString("UITAB", "Purchased");
        if (PlayCloudDataManager.Instance != null)
            PlayCloudDataManager.Instance.SetCurToLoaded();
        SetHavingEquip();
    }
    public void OnSlotClick(Slot curSlot)
    {
        if(curSlot.ishave == false)
        {
            checkPanel.SetActive(true);
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(() => { CheckYesBtn(curSlot); });
        }
        else
        {
            equipPanel.SetActive(true);
            confirmBtn.onClick.RemoveAllListeners();
            confirmBtn.onClick.AddListener(() => { EquipYesBtn(curSlot); });
        }
        
    }
    public void CheckYesBtn(Slot curSlot)
    {
        switch (curSlot.part)
        {
            case Part.Weapon:
                if(GameManager.instance.dia >= (ulong)weaponCos[curSlot.itemNum].price)
                {
                    haveWeapon.Add(curSlot.itemNum);
                    GameManager.instance.dia -= (ulong)weaponCos[curSlot.itemNum].price;
                    curSlot.ishave = true;
                    Debug.Log(curSlot.ishave);
                    curSlot.btnImage.sprite = changeImage;
                    //curSlot.price_Text.enabled = false;
                    curSlot.price_Text.color = Color.black;
                    curSlot.price_Text.text = $"{lPurchased.GetLocalizedString()}";
                    curSlot.dia_Image.enabled = false;
                }
                break;
            case Part.Shield:
                if (GameManager.instance.dia >= (ulong)shieldCos[curSlot.itemNum].price)
                {
                    haveShield.Add(curSlot.itemNum);
                    GameManager.instance.dia -= (ulong)shieldCos[curSlot.itemNum].price;
                    curSlot.ishave = true;
                    Debug.Log(curSlot.ishave);
                    curSlot.btnImage.sprite = changeImage;
                    //curSlot.price_Text.enabled = false;
                    curSlot.price_Text.color = Color.black;
                    curSlot.price_Text.text = $"{lPurchased.GetLocalizedString()}";
                    curSlot.dia_Image.enabled = false;
                }
                break;
            case Part.Helmet:
                if (GameManager.instance.dia >= (ulong)helmetCos[curSlot.itemNum].price)
                {
                    haveHelmet.Add(curSlot.itemNum);
                    GameManager.instance.dia -= (ulong)helmetCos[curSlot.itemNum].price;
                    curSlot.ishave = true;
                    Debug.Log(curSlot.ishave);
                    curSlot.btnImage.sprite = changeImage;
                    //curSlot.price_Text.enabled = false;
                    curSlot.price_Text.color = Color.black;
                    curSlot.price_Text.text = $"{lPurchased.GetLocalizedString()}";
                    curSlot.dia_Image.enabled = false;
                }
                break;
        }
        checkPanel.SetActive(false);
        UIManager.Instance.ShowDiaCount();
    }
    public void CheckNoBtn()
    {
        checkPanel.SetActive(false);
    }

    public void EquipYesBtn(Slot curSlot)
    {
        equipPanel.SetActive(false);
        switch (curSlot.part)
        {
            case Part.Weapon:
                GameManager.instance.playerEquips[0] = curSlot.itemNum;
                GameManager.instance.player.weapon.GetComponent<SpriteRenderer>().sprite = weaponCos[curSlot.itemNum].sprite;
                break;
            case Part.Shield:
                GameManager.instance.playerEquips[1] = curSlot.itemNum;
                GameManager.instance.player.shield.GetComponent<SpriteRenderer>().sprite = shieldCos[curSlot.itemNum].sprite;
                break;
            case Part.Helmet:
                GameManager.instance.playerEquips[2] = curSlot.itemNum;
                GameManager.instance.player.helmet.GetComponent<SpriteRenderer>().sprite = helmetCos[curSlot.itemNum].sprite;
                break;
        }        
    }
    public void EquipNoBtn()
    {
        equipPanel.SetActive(false);
    }
    public void SetEquip()
    {
        GameManager.instance.player.weapon.GetComponent<SpriteRenderer>().sprite = weaponCos[GameManager.instance.playerEquips[0]].sprite;
        GameManager.instance.player.shield.GetComponent<SpriteRenderer>().sprite = shieldCos[GameManager.instance.playerEquips[1]].sprite;
        GameManager.instance.player.helmet.GetComponent<SpriteRenderer>().sprite = helmetCos[GameManager.instance.playerEquips[2]].sprite;
    }
    public void SetHavingEquip()
    {
        Transform[] objs = weaponParent.GetComponentsInChildren<Transform>();
        foreach(Transform obj in objs)
        {
            if (obj == weaponParent) continue;
            Destroy(obj.gameObject);
        }
        objs = shieldParent.GetComponentsInChildren<Transform>();
        foreach (Transform obj in objs)
        {
            if (obj == shieldParent) continue;
            Destroy(obj.gameObject);
        }
        objs = helmetParent.GetComponentsInChildren<Transform>();
        foreach (Transform obj in objs)
        {
            if (obj == helmetParent) continue;
            Destroy(obj.gameObject);
        }
        for (int i = 0; i < weaponCos.Length; i++)
        {
            GameObject go_item = Instantiate(go_Item, weaponParent);
            //weaponCos[i].num = i;
            //if(i < 3)
            //    weaponCos[i].price = i * 200;
            //else
            //    weaponCos[i].price = 100 + i * 350;
            //EditorUtility.SetDirty(weaponCos[i]);
            Slot sm = go_item.GetComponent<Slot>();
            if (haveWeapon.Contains(weaponCos[i].num))
            {
                sm.ishave = true;
                sm.btnImage.sprite = changeImage;
                //sm.price_Text.enabled = false;
                sm.price_Text.color = Color.black;
                sm.price_Text.text = $"{lPurchased.GetLocalizedString()}";
                sm.dia_Image.enabled = false;
            }
            else
            {
                sm.ishave = false;
                sm.price_Text.text = weaponCos[i].price.ToString();
            }
            sm.itemNum = weaponCos[i].num;
            sm.part = weaponCos[i].part;
            sm.item_Image.sprite = weaponCos[i].sprite;
            sm.item_Image.rectTransform.localRotation = Quaternion.Euler(0f, 0f, -45);
            sm.item_Image.rectTransform.localScale = new Vector3(0.4f * weaponCos[i].sprite.rect.width / 7,
                    0.9f * weaponCos[i].sprite.rect.height / 18, 1);
            sm.slotBtn.onClick.AddListener(() => { OnSlotClick(sm); });
        }

        for (int i = 0; i < shieldCos.Length; i++)
        {
            GameObject go_item = Instantiate(go_Item, shieldParent);
            //shieldCos[i].num = i;
            //if (i < 3)
            //    shieldCos[i].price = i * 200;
            //else
            //    shieldCos[i].price = 100 + i * 350;
            //EditorUtility.SetDirty(shieldCos[i]);
            Slot sm = go_item.GetComponent<Slot>();
            if (haveShield.Contains(shieldCos[i].num))
            {
                sm.ishave = true;
                sm.btnImage.sprite = changeImage;
                //sm.price_Text.enabled = false;
                sm.price_Text.color = Color.black;
                sm.price_Text.text = $"{lPurchased.GetLocalizedString()}";
                sm.dia_Image.enabled = false;
            }
            else
            {
                sm.ishave = false;
                sm.price_Text.text = shieldCos[i].price.ToString();
            }
            sm.itemNum = shieldCos[i].num;
            sm.part = shieldCos[i].part;
            sm.item_Image.sprite = shieldCos[i].sprite;
            sm.item_Image.rectTransform.localScale = new Vector3(0.6f * shieldCos[i].sprite.rect.width / 14,
                    0.6f * shieldCos[i].sprite.rect.height / 14, 1);
            sm.slotBtn.onClick.AddListener(() => { OnSlotClick(sm); });
        }

        for (int i = 0; i < helmetCos.Length; i++)
        {
            GameObject go_item = Instantiate(go_Item, helmetParent);
            //helmetCos[i].num = i;
            //if (i < 3)
            //    helmetCos[i].price = i * 200;
            //else
            //    helmetCos[i].price = 100 + i * 350;
            //EditorUtility.SetDirty(helmetCos[i]);
            Slot sm = go_item.GetComponent<Slot>();
            if (haveHelmet.Contains(helmetCos[i].num))
            {
                sm.ishave = true;
                sm.btnImage.sprite = changeImage;
                //sm.price_Text.enabled = false;
                sm.price_Text.color = Color.black;
                sm.price_Text.text = $"{lPurchased.GetLocalizedString()}";
                sm.dia_Image.enabled = false;
            }
            else
            {
                sm.ishave = false;
                sm.price_Text.text = helmetCos[i].price.ToString();
            }
            sm.itemNum = helmetCos[i].num;
            sm.part = helmetCos[i].part;
            sm.item_Image.sprite = helmetCos[i].sprite;
            sm.item_Image.rectTransform.localScale = new Vector3(1.2f * helmetCos[i].sprite.rect.width / 32,
                    1.2f * helmetCos[i].sprite.rect.height / 32, 1);
            sm.slotBtn.onClick.AddListener(() => { OnSlotClick(sm); });
        }
    }
}