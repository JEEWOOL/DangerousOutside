using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableThis : MonoBehaviour
{
    public void DisableThisButton(GameObject todis)
    {
        todis.SetActive(false);
    }
}
