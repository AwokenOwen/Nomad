using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData
{
    public string name;

    [SerializeField]
    PlayerData playerData;

    [SerializeField]
    SettingsData settingsData;

    [SerializeField]
    Vector3 spawnPoint;

    public WorldData(string name)
    {
        this.name = name;
        //imput world spawn point here
        spawnPoint = Vector3.zero;
        this.playerData = new PlayerData(spawnPoint);
        this.settingsData = new SettingsData();
    }

    public void saveWorld()
    {
        playerData.PlayerInventory.SaveItems();
    }

    public void loadWorld(out PlayerData player)
    {
        player = playerData;
        playerData.PlayerInventory.LoadItems();
    }

    public float getSens()
    {
        return settingsData.Sensitivity;
    }
}
