using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class MainMenuUIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SingleplayerMenu;
    public GameObject MultiplayerMenu;
    public GameObject NewSingleplayerWorldMenu;

    #region Singleplayer Variables
    [Header("Singleplayer Variables")]
    public List<GameObject> worldSaveObjects;

    public GameObject worldSavePrefab;
    public GameObject singleplayerContentObject;

    public TMP_InputField newSingleplayerWorldNameField;

    public Button openWorldButton;
    public TMP_Text openWorldText; 
    public Button deleteWorldButton;
    public TMP_Text deleteWorldText;

    string selectedWorld;
    #endregion

    private void OnEnable()
    {
        WorldSaveScript.OnSelect += WorldSelected;
    }

    private void Start()
    {
        CloseAll();
        OpenMainMenu();
    }

    #region Menu Transition

    void CloseAll()
    {
        MainMenu.SetActive(false);
        SingleplayerMenu.SetActive(false);
        MultiplayerMenu.SetActive(false);
        NewSingleplayerWorldMenu.SetActive(false);
        foreach (GameObject obj in worldSaveObjects)
        {
            Destroy(obj);
        }
        worldSaveObjects = new List<GameObject>();
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

        foreach (GameObject obj in worldSaveObjects)
        {
            if (!obj.GetComponent<WorldSaveScript>().worldTitle.text.Equals(name))
            {
                obj.GetComponent<WorldSaveScript>().selectedImage.SetActive(false);
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
            obj.GetComponent<WorldSaveScript>().lastPlayed.text = save.lastPlayed;

            worldSaveObjects.Add(obj);
        }
    }

    void RefreshWorlds()
    {
        foreach (GameObject obj in worldSaveObjects)
        {
            Destroy(obj);
        }
        worldSaveObjects = new List<GameObject>();

        InstantiateWorldSaveObjects();
    }

    public void DeleteWorld()
    {
        FileManager.DeleteSave(selectedWorld);
        RefreshWorlds();
    }
}
