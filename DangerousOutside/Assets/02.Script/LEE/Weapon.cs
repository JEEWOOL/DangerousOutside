using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ulong damage;

    public void SetDamage(ulong damage)
    {
        this.damage = damage;
    }
}
