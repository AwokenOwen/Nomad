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

    public void saveWorld(Vector3 playerPos)
    {
        playerData.PlayerInventory.SaveItems();
        playerData.Position = playerPos;
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

    public void AddItem(ItemData data)
    {
        playerData.PlayerInventory.AddItem(data, 1);
    }

    public float GetMoveSpeed()
    {
        //do stat stuff for new MoveSpeed
        return BASE_STATS.MOVE_SPEED;
    }

    public float GetJumpForce()
    {
        //do stat stuff for new JumpForce
        return BASE_STATS.JUMP_FORCE;
    }

    public AbilityData GetAbilities()
    {
        return playerData.Abilities;
    }

    public Vector3 GetSpawn()
    {
        return playerData.Position;
    }
}
