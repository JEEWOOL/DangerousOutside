using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDown : MonoBehaviour
{
    public bool m_IsButtonDowning;
    public Button button;

    private float ptime = 0f;
    private float dtime = 0.25f;
    void Update()
    {
        if (m_IsButtonDowning)
        {
            ptime += Time.deltaTime;
            if(ptime > dtime)
                button.onClick.Invoke();
        }
        else
        {
            ptime = 0f;
        }
    }

    public void PointerDown()
    {
        m_IsButtonDowning = true;
    }

    public void PointerUp()
    {
        m_IsButtonDowning = false;
    }
}
