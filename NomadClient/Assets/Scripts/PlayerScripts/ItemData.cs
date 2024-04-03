using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string name;
    public float weight;

    public ItemData(string name, float weight) 
    { 
        this.name = name;
        this.weight = weight;
    }
}
