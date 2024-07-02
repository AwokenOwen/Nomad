using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    Weapon,
    Armor,
    Material
}


[System.Serializable]
public class ItemData
{
    public string name;
    public float weight;
    public ItemType type;

    public ItemData(string name, float weight, ItemType type) 
    { 
        this.name = name;
        this.weight = weight;
        this.type = type;
    }
}
