using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundClick : MonoBehaviour
{
    public TMP_Text text;

    public void Start()
    {
        SoundManager.Instance.SetText(text);
    }
    public void SubSound()
    {
        SoundManager.Instance.SoundGostop(text);
    }
}
