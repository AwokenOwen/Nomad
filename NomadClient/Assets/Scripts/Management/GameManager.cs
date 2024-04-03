using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    MainMenu,
    SingleplayerWorld,
    MultiplayerWorld
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStates currentGameState { get; private set; }

    public WorldData currentWorldData;

    public delegate void WorldSelectAction(string name);

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentGameState = GameStates.MainMenu;
    }

    private void Update()
    {
        switch (currentGameState)
        {
            case GameStates.MainMenu:
                break;
            case GameStates.SingleplayerWorld:
                SingleplayerUpdate();
                break;
            case GameStates.MultiplayerWorld:
                break;
        }
    }

    #region MainMenu
    public void CreateNewSingleplayerWorld(string name)
    {
        FileManager.saveWorld(new WorldData(name));
        OpenSingleplayerWorld(name);
    }

    public void OpenSingleplayerWorld(string name)
    {
        currentWorldData = FileManager.loadWorld(name);
        //switch to singleplayer mode
        currentGameState = GameStates.SingleplayerWorld;
        //load singleplayer scene
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

    #region Singleplayer
    private void SingleplayerUpdate()
    {
        //single player update
    }


    #endregion
}
