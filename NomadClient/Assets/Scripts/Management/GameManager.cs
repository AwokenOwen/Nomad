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

    public delegate void ChangeInputStateAction(InputMode mode);
    public static event ChangeInputStateAction ChangeInputEvent;

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
        DontDestroyOnLoad(this.gameObject);
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
        WorldData data = new WorldData(name);

        data.AddItem(ITEMS.SIMPLE_SWORD);

        FileManager.saveWorld(data);
        OpenSingleplayerWorld(name);
    }

    public void OpenSingleplayerWorld(string name)
    {
        currentWorldData = FileManager.loadWorld(name);
        //switch to singleplayer mode
        currentGameState = GameStates.SingleplayerWorld;
        //load singleplayer scene
        SceneManager.LoadScene("SingleplayerWorld");

        ChangeInput(InputMode.Game);
    }
    #endregion

    #region Singleplayer
    private void SingleplayerUpdate()
    {
        
    }

    public void setSens(float sens)
    {
        currentWorldData.setSens(sens);
    }
    #endregion

    private void OnApplicationQuit()
    {
        switch (currentGameState)
        {
            case GameStates.MainMenu:
                break;
            case GameStates.SingleplayerWorld:
                currentWorldData.saveWorld(PlayerManager.instance.transform.position);
                FileManager.saveWorld(currentWorldData);
                break;
            case GameStates.MultiplayerWorld:
                break;
        }
    }

    public void ChangeInput(InputMode mode)
    {
        ChangeInputEvent(mode);
    }
}
