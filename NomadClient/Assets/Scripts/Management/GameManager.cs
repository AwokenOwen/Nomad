using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region Events
    public delegate void NewSingleplayerWorldAction();
    public static event NewSingleplayerWorldAction OnNewSingleplayerWorld;

    public delegate void OpenSingleplayerWorldAction();
    public static event OpenSingleplayerWorldAction OnOpenSingleplayerWorld;

    public delegate void SelectedWorldAction(WorldData data);
    public static event SelectedWorldAction OnSelectedWorld;
    #endregion

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
                MainMenuUpdate();
                break;
            case GameStates.SingleplayerWorld:
                break;
            case GameStates.MultiplayerWorld:
                break;
        }
    }

    #region MainMenu
    private void MainMenuUpdate()
    {
        //Main Menu Update
    }

    public void CreateNewSingleplayerWorld()
    {
        OnNewSingleplayerWorld();
    }

    public void OpenSingleplayerWorld()
    {
        OnOpenSingleplayerWorld();
    }

    public void SelectedWorld(WorldData data)
    {
        OnSelectedWorld(data);
    }

    #endregion
}
