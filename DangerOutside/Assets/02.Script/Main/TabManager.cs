using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public GameObject powerUpPanel;
    public GameObject costumePanel;
    public GameObject newLifePanel;
    public GameObject storePanel;

    public TMP_Text powerUpPanel_Text;
    public TMP_Text storePanel_Text;
    public TMP_Text costumePanel_Text;
    public TMP_Text newLifePanel_Text;

    // 내부(상점) 미니판넬    
    public GameObject inAppPanel;    
    public TMP_Text inAppTab_Text;   
    public GameObject SupportPanel;    
    public TMP_Text SupportTab_Text;

    // 내부(장비) 미니판넬
    public GameObject equipWeaponPanel;
    public GameObject equipShieldPanel;
    public GameObject equipHelmetPanel;    

    public TMP_Text equipWeaponTab_Text;
    public TMP_Text equipShieldTab_Text;
    public TMP_Text equipHelmetTab_Text;
    

    public void ChangePannel(int num)
    {
        if(num != 0)
        {
            powerUpPanel.SetActive(false);
            powerUpPanel_Text.color = Color.white;
        }            
        if(num == 0)
        {
            powerUpPanel.SetActive(true);
            powerUpPanel_Text.color = new Color32(118, 0, 0, 255);
        }   
        
        if(num != 1)
        {
            costumePanel.SetActive(false);
            costumePanel_Text.color = Color.white;
        }            
        if(num == 1)
        {
            costumePanel.SetActive(true);
            costumePanel_Text.color = new Color32(118, 0, 0, 255);
        }
            
        if(num != 2)
        {
            newLifePanel.SetActive(false);
            newLifePanel_Text.color = Color.white;
        }            
        if(num == 2)
        {
            newLifePanel.SetActive(true);
            newLifePanel_Text.color = new Color32(118, 0, 0, 255);
            NewLifeManager.Instance.SetText();
        }
            
        if(num != 3)
        {
            storePanel.SetActive(false);
            storePanel_Text.color = Color.white;
        }            
        if(num == 3)
        {
            storePanel.SetActive(true);
            storePanel_Text.color = new Color32(118, 0, 0, 255);
            UIManager.Instance.ShowStoreText();
        }
    }

    public void StoreInPanelChange(int num)
    {
        if (num != 0)
        {
            inAppPanel.SetActive(false);
            inAppTab_Text.color = Color.white;
        }
        if (num == 0)
        {
            inAppPanel.SetActive(true);
            inAppTab_Text.color = new Color32(118, 0, 0, 255);
        }
        if (num != 1)
        {
            SupportPanel.SetActive(false);
            SupportTab_Text.color = Color.white;
        }
        if (num == 1)
        {
            SupportPanel.SetActive(true);
            SupportTab_Text.color = new Color32(118, 0, 0, 255);
        }
    }

    public void CostumeInPanelChange(int num)
    {
        if (num != 0)
        {
            equipWeaponPanel.SetActive(false);
            equipWeaponTab_Text.color = Color.white;
        }
        if (num == 0)
        {
            equipWeaponPanel.SetActive(true);
            equipWeaponTab_Text.color = new Color32(118, 0, 0, 255);
        }

        if (num != 1)
        {
            equipShieldPanel.SetActive(false);
            equipShieldTab_Text.color = Color.white;
        }
        if (num == 1)
        {
            equipShieldPanel.SetActive(true);
            equipShieldTab_Text.color = new Color32(118, 0, 0, 255);
        }

        if (num != 2)
        {
            equipHelmetPanel.SetActive(false);
            equipHelmetTab_Text.color = Color.white;
        }
        if (num == 2)
        {
            equipHelmetPanel.SetActive(true);
            equipHelmetTab_Text.color = new Color32(118, 0, 0, 255);
        }
    }
}
