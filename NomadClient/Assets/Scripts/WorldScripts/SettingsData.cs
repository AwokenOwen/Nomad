using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float Sensitivity;

    public SettingsData() 
    { 
        Sensitivity = 0.005f;
    }
}
