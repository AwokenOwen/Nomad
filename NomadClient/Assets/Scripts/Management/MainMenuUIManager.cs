using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum MenuState
{
    MainMenu,
    SingleplayerNonSelected,
    SingleplayerSelect,
    SingleplayerSelected
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
    #endregion

    private void Start()
    {
        CloseAll();
        OpenMainMenu();
        menuState = MenuState.MainMenu;
        eventSystem.SetSelectedGameObject(MainMenuButtons[0]);
        
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
    }

    #endregion

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

        openWorldButton.interactable = true;
        openWorldText.alpha = 1f;
        deleteWorldButton.interactable = true;
        deleteWorldText.alpha = 1f;
    }

    void WorldDeselect()
    {
        selectedWorld = null;

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

    public void nextSelectedItem()
    {
        switch (menuState)
        {
            case MenuState.MainMenu:
                break;
            case MenuState.SingleplayerNonSelected:
                break;
            case MenuState.SingleplayerSelect:
                break;
            case MenuState.SingleplayerSelected:
                break;
        }
    }
}
