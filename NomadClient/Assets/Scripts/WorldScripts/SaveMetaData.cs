using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SaveMetaData
{
    public string name;
    public string lastPlayed;

    public SaveMetaData(string name, string lastPlayed)
    {
        this.name = name;
        this.lastPlayed = lastPlayed;   
    }
}
