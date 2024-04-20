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
    public InventoryData PlayerInventory;
    public StatsData PlayerStats;
    public AbilityData Abilities;

    public Vector3 Position;

    public PlayerData(Vector3 Position)
    {
        this.PlayerInventory = new InventoryData();
        this.PlayerStats = new StatsData();
        this.Abilities = new AbilityData(new JumpData());

        this.Position = Position;
    }
}
