using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grimore
{
    public JumpData JumpData;
    public InventoryData PlayerInventory;
    public StatsData PlayerStats;

    public WeaponData[] EquiptedWeapons;
    public WeaponData[] HeldWeapons;

    public Grimore()
    {
        this.JumpData = new JumpData("default_jump");
        this.PlayerInventory = new InventoryData();
        this.PlayerStats = new StatsData();
        EquiptedWeapons = new WeaponData[3];
        for (int i = 0; i < EquiptedWeapons.Length; i++)
        {
            EquiptedWeapons[i] = ITEMS.FIST;
        }
        HeldWeapons = new WeaponData[2];
        for (int i = 0;i < HeldWeapons.Length; i++)
        {
            HeldWeapons[i] = ITEMS.FIST;
        }
    }
}
