using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Euipment Asset", menuName = "Scriptable Object/Euipment Asset", order = int.MaxValue)]
public class EquipmentAsset : ScriptableObject
{
    public int rate;
    public int price;
    public Color color;
    public Sprite sprite;
}
