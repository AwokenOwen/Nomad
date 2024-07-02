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
    Vector3 spawnPoint;

    public WorldData(string name)
    {
        this.name = name;
        //input world spawn point here
        spawnPoint = new Vector3(2274f, 11f, 2721f);
        this.playerData = new PlayerData(spawnPoint);
    }

    public void saveWorld(Vector3 playerPos)
    {
        playerData.Grimore.PlayerInventory.SaveItems();
        playerData.Position = playerPos;
    }

    public void loadWorld(out PlayerData player)
    {
        player = playerData;
        playerData.Grimore.PlayerInventory.LoadItems();
    }

    public void AddItem(ItemData data)
    {
        playerData.Grimore.PlayerInventory.AddItem(data, 1);
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

    public Grimore GetGrimore()
    {
        return playerData.Grimore;
    }

    public Vector3 GetSpawn()
    {
        return playerData.Position;
    }
}
