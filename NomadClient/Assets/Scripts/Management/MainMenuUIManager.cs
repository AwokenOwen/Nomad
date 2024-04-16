using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public enum MenuState
{
    MainMenu,
    SingleplayerNonSelected,
    SingleplayerSelect,
    SingleplayerSelected,
    SingleplayerNewGame,
}

public class MainMenuUIManager : MonoBehaviour
{
    private MenuState menuState;

    public GameObject MainMenu;
    public GameObject SingleplayerMenu;
    public GameObject MultiplayerMenu;
    public GameObject NewSingleplayerWorldMenu;

    public EventSystem eventSystem;

    #region Main Menu Variables
    [Header("Main Menu Variables")]
    [SerializeField]
    private GameObject[] MainMenuButtons;
    #endregion

    #region Singleplayer Variables
    [Header("Singleplayer Variables")]
    [SerializeField]
    private GameObject[] SingleplayerTopRow;
    [SerializeField]
    private GameObject[] SingleplayerBottomRow;

    public List<WorldSaveScript> worldSaveObjects;

    public GameObject worldSavePrefab;
    public GameObject singleplayerContentObject;

    public TMP_InputField newSingleplayerWorldNameField;

    public Button openWorldButton;
    public TMP_Text openWorldText; 
    public Button deleteWorldButton;
    public TMP_Text deleteWorldText;

    string selectedWorld;

    int currentSelectedIndex;
    #endregion

    private void Awake()
    {
        GameManager.MenuNavigationEvent += Navigation;
        GameManager.MenuSubmitEvent += Submit;
    }

    private void OnDisable()
    {
        GameManager.MenuNavigationEvent -= Navigation;
        GameManager.MenuSubmitEvent -= Submit;
    }

    private void Start()
    {
        CloseAll();
        OpenMainMenu();
        menuState = MenuState.MainMenu;
        eventSystem.SetSelectedGameObject(MainMenuButtons[0]);
        currentSelectedIndex = 0;
    }

    #region Menu Transition

    void CloseAll()
    {
        MainMenu.SetActive(false);
        SingleplayerMenu.SetActive(false);
        MultiplayerMenu.SetActive(false);
        NewSingleplayerWorldMenu.SetActive(false);
        foreach (WorldSaveScript obj in worldSaveObjects)
        {
            Destroy(obj.gameObject);
        }
        worldSaveObjects = new List<WorldSaveScript>();
    }

    public void OpenMultiplayerMenu()
    {
        CloseAll();
        MultiplayerMenu.SetActive(true);
    }

    public void OpenSingleplayerMenu()
    {
        CloseAll();
        SingleplayerMenu.SetActive(true);
        InstantiateWorldSaveObjects();
        menuState = MenuState.SingleplayerNonSelected;
        currentSelectedIndex = 0;
        eventSystem.SetSelectedGameObject(SingleplayerTopRow[0]);
    }
    public void OpenMainMenu()
    {
        CloseAll();
        MainMenu.SetActive(true);
    }

    public void OpenNewWorldMenu()
    {
        CloseAll();
        NewSingleplayerWorldMenu.SetActive(true);
        currentSelectedIndex = 0;
        menuState = MenuState.SingleplayerNewGame;
        eventSystem.SetSelectedGameObject(newSingleplayerWorldNameField.gameObject);
    }

    #endregion

    #region Singleplayer World Management
    public void CreateNewWorld()
    {
        if (FileManager.checkIfValidWorldName(newSingleplayerWorldNameField.text))
        {
            CloseAll();
            GameManager.instance.CreateNewSingleplayerWorld(newSingleplayerWorldNameField.text);
        }
    }

    public void OpenWorld()
    {
        CloseAll();
        GameManager.instance.OpenSingleplayerWorld(selectedWorld);
    }

    void WorldSelected(string name)
    {
        selectedWorld = name;

        foreach (WorldSaveScript obj in worldSaveObjects)
        {
            if (!obj.worldTitle.text.Equals(name))
            {
                obj.selectedImage.SetActive(false);
            }
        }
    }

    void WorldDeselect()
    {
        WorldSelected("");

        openWorldButton.interactable = false;
        openWorldText.alpha = 87f/255f;
        deleteWorldButton.interactable = false;
        deleteWorldText.alpha = 87f/255f;
    }

    public void InstantiateWorldSaveObjects()
    {
        WorldDeselect();
        List<SaveMetaData> saves = FileManager.getSaves();

        foreach (SaveMetaData save in saves)
        {
            GameObject obj = Instantiate(worldSavePrefab, singleplayerContentObject.transform);
            obj.GetComponent<WorldSaveScript>().worldTitle.text = save.name;
            obj.GetComponent<WorldSaveScript>().lastPlayed.text = "Last Played: " + save.lastPlayed;

            worldSaveObjects.Add(obj.GetComponent<WorldSaveScript>());
        }
    }

    void RefreshWorlds()
    {
        foreach (WorldSaveScript obj in worldSaveObjects)
        {
            Destroy(obj.gameObject);
        }
        worldSaveObjects = new List<WorldSaveScript>();

        InstantiateWorldSaveObjects();
    }

    public void DeleteWorld()
    {
        FileManager.DeleteSave(selectedWorld);
        RefreshWorlds();
    }

    #endregion

    void Navigation(Vector2 input)
    {
        switch (menuState)
        {
            case MenuState.MainMenu:
                currentSelectedIndex -= Mathf.RoundToInt(input.y);
                if (currentSelectedIndex < 0)
                    currentSelectedIndex = MainMenuButtons.Length - 1;
                currentSelectedIndex = currentSelectedIndex % MainMenuButtons.Length;
                eventSystem.SetSelectedGameObject(MainMenuButtons[currentSelectedIndex]);
                break;
            case MenuState.SingleplayerNonSelected:
                if (input.y < 0 && worldSaveObjects.Count > 0)
                {
                    eventSystem.SetSelectedGameObject(null);
                    menuState = MenuState.SingleplayerSelect;
                    currentSelectedIndex = 0;
                    WorldSelected(worldSaveObjects[0].worldTitle.text);
                    worldSaveObjects[0].selectedImage.SetActive(true);
                    WorldDeselect();
                    break;
                }
                currentSelectedIndex -= Mathf.RoundToInt(input.x);
                if (currentSelectedIndex < 0)
                    currentSelectedIndex = SingleplayerTopRow.Length - 1;
                currentSelectedIndex = currentSelectedIndex % SingleplayerTopRow.Length;
                eventSystem.SetSelectedGameObject(SingleplayerTopRow[currentSelectedIndex]);
                break;
            case MenuState.SingleplayerSelect:
                currentSelectedIndex -= Mathf.RoundToInt(input.y);
                if (currentSelectedIndex < 0)
                {
                    menuState = MenuState.SingleplayerNonSelected;
                    currentSelectedIndex = 0;
                    eventSystem.SetSelectedGameObject(SingleplayerTopRow[0]);
                }
                currentSelectedIndex = currentSelectedIndex % worldSaveObjects.Count;
                WorldSelected(worldSaveObjects[currentSelectedIndex].worldTitle.text);
                worldSaveObjects[currentSelectedIndex].selectedImage.SetActive(true);
                break;
            case MenuState.SingleplayerSelected:
                currentSelectedIndex -= Mathf.RoundToInt(input.x);
                if (currentSelectedIndex < 0)
                    currentSelectedIndex = SingleplayerBottomRow.Length - 1;
                currentSelectedIndex = currentSelectedIndex % SingleplayerBottomRow.Length;
                eventSystem.SetSelectedGameObject(SingleplayerBottomRow[currentSelectedIndex]);
                break;
            case MenuState.SingleplayerNewGame:
                //no navigation
                break;
        }
    }

    void Submit()
    {
        switch (menuState)
        {
            case MenuState.MainMenu:
                eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                break;
            case MenuState.SingleplayerNonSelected:
                eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                break;
            case MenuState.SingleplayerSelect:
                openWorldButton.interactable = true;
                openWorldText.alpha = 1f;
                deleteWorldButton.interactable = true;
                deleteWorldText.alpha = 1f;

                eventSystem.SetSelectedGameObject(SingleplayerBottomRow[0]);
                currentSelectedIndex = 0;
                menuState = MenuState.SingleplayerSelected;
                break;
            case MenuState.SingleplayerSelected:
                eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                break;
            case MenuState.SingleplayerNewGame:
                CreateNewWorld();
                break;
        }
    }
}
