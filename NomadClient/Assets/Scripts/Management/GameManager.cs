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
    [field: SerializeField] public GameStates currentGameState { get; private set; }

    //datas
    public WorldData currentWorldData;
    [SerializeField] SettingsData settingsData;

    public delegate void ChangeInputStateAction(InputMode mode);
    public static event ChangeInputStateAction ChangeInputEvent;

    public delegate void MenuNavigationAction(Vector2 input);
    public static event MenuNavigationAction MenuNavigationEvent;

    public delegate void MenuTavNavAction(float value);
    public static event MenuTavNavAction MenuTabNavEvent;

    public delegate void MenuSubmitAction();
    public static event MenuSubmitAction MenuSubmitEvent;

    public delegate void MenuBackAction();
    public static event MenuBackAction MenuBackEvent;

    public delegate void MenuCloseAction();
    public static event MenuCloseAction MenuCloseEvent;

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
        settingsData = FileManager.getSettings();
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

        data.saveWorld(data.GetSpawn());

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
        settingsData.Sensitivity = sens;
    }

    public float getSens()
    {
        return settingsData.Sensitivity;
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
                FileManager.SaveSettings(settingsData);
                break;
            case GameStates.MultiplayerWorld:
                break;
        }
    }

    public void ChangeInput(InputMode mode)
    {
        ChangeInputEvent(mode);
    }

    public void MenuNavigate(Vector2 input)
    {
        MenuNavigationEvent(input);
    }

    public void MenuTabNav(float value)
    {
        MenuTabNavEvent(value);
    }

    public void MenuSubmit()
    {
        MenuSubmitEvent();
    }


    public void MenuBack()
    {
        MenuBackEvent();
    }

    public void MenuClose()
    {
        MenuCloseEvent();
    }
}
