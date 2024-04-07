using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    public JumpData JumpData;

    public AbilityData(JumpData JumpData)
    {
        this.JumpData = JumpData;
    }
}
