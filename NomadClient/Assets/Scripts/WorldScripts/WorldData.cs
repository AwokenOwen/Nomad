using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData
{
    public string name;
    
    public string lastPlayed;

    public WorldData(string name, string lastPlayed)
    {
        this.name = name;
        this.lastPlayed = lastPlayed;
    }
}
