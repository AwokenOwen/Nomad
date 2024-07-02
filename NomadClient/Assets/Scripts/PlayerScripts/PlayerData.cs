using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats
{
    HEALTH,
    DEFENSE,
    STAMINA,
    PHYSICAL_STRENGTH,
    MAGICAL_STRENGTH
}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public Grimore Grimore;
    public Vector3 Position;

    public PlayerData(Vector3 Position)
    {
        this.Grimore = new Grimore();
        this.Position = Position;
    }
}
