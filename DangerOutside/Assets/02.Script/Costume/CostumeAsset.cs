using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Costume Asset", menuName = "Scriptable Object/Costume Asset", order = int.MaxValue)]
public class CostumeAsset : ScriptableObject
{
    public int num;
    public int price;
    public Sprite sprite;
    public Part part;
    public bool ishave = false;
}
public enum Part
{
    Weapon,
    Shield,
    Helmet
};