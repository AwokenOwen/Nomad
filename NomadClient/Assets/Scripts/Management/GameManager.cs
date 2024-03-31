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

    }

    public void CreateNewSingleplayerWorld()
    {

    }

    public void OpenSingleplayerWorld()
    {

    }

    #endregion
}
