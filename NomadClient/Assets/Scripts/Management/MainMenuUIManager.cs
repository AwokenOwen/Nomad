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

    [Header("Singleplayer Variables")]
    public List<WorldData> saves;

    public List<GameObject> saveObjects;

    public GameObject worldSaveObject;
    public GameObject singleplayerContentObject;

    public TMP_InputField newSingleplayerWorldNameField;

    //open delete buttons
    public Button openWorldButton;
    public TMP_Text openWorldText; 
    public Button deleteWorldButton;
    public TMP_Text deleteWorldText;

    public WorldData selectedWorld;

    private void OnEnable()
    {
        GameManager.OnNewSingleplayerWorld += CreateNewWorld;
        GameManager.OnSelectedWorld += WorldSelected;
    }

    private void OnDisable()
    {
        GameManager.OnNewSingleplayerWorld -= CreateNewWorld;
        GameManager.OnSelectedWorld -= WorldSelected;
    }

    private void Start()
    {
        CloseAll();
        OpenMainMenu();
    }

    void CloseAll()
    {
        MainMenu.SetActive(false);
        SingleplayerMenu.SetActive(false);
        //MultiplayerMenu.SetActive(false);
        NewSingleplayerWorldMenu.SetActive(false);
        selectedWorld = null;
        foreach (GameObject obj in saveObjects)
        {
            Destroy(obj);
        }
        saveObjects = new List<GameObject>();
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

    public void CreateNewWorld()
    {
        if (FileManager.checkIfValidWorldName(newSingleplayerWorldNameField.text))
        {
            FileManager.newGame(newSingleplayerWorldNameField.text, FileManager.getLastPlayed(DateTime.Now));
            OpenMainMenu();
        }
    }

    void WorldSelected(WorldData data)
    {
        selectedWorld = data;

        foreach (GameObject obj in saveObjects)
        {
            if (!obj.GetComponent<WorldSaveScript>().worldData.name.Equals(data.name))
            {
                obj.GetComponent<WorldSaveScript>().selectedImage.SetActive(false);
            }
        }

        openWorldButton.interactable = true;
        openWorldText.alpha = 255f;
        deleteWorldButton.interactable = true;
        deleteWorldText.alpha = 255f;
    }

    void WorldDeselect()
    {
        selectedWorld = null;

        openWorldButton.interactable = false;
        openWorldText.alpha = 87f;
        deleteWorldButton.interactable = false;
        deleteWorldText.alpha = 87f;
    }

    public void InstantiateWorldSaveObjects()
    {
        WorldDeselect();
        saves = FileManager.getSaves();
        foreach (WorldData data in saves)
        {
            GameObject obj = Instantiate(worldSaveObject, singleplayerContentObject.transform);
            obj.GetComponent<WorldSaveScript>().worldData = data;
            obj.GetComponent<WorldSaveScript>().worldTitle.text = data.name;
            obj.GetComponent<WorldSaveScript>().lastPlayed.text = "Last Played: " + data.lastPlayed;
            saveObjects.Add(obj);
        }
    }
}
