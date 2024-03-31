using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class MainMenuUIManager : MonoBehaviour
{
    public static MainMenuUIManager instance;

    public GameObject MainMenu;
    public GameObject SingleplayerMenu;
    public GameObject MultiplayerMenu;
    public GameObject NewGameMenu;

    [Header("Singleplayer Variables")]
    public List<WorldData> saves;

    public List<GameObject> saveObjects;

    public GameObject worldSaveObject;
    public GameObject singleplayerContentObject;

    public TMP_InputField newGameName;

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
        CloseAll();
        MainMenuButton();
    }

    public void getWorldSaves()
    {
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

    void CloseAll()
    {
        MainMenu.SetActive(false);
        SingleplayerMenu.SetActive(false);
        //MultiplayerMenu.SetActive(false);
        NewGameMenu.SetActive(false);
        foreach (GameObject obj in saveObjects)
        {
            Destroy(obj);
        }
    }

    public void SingleplayerButton()
    {
        CloseAll();
        SingleplayerMenu.SetActive(true);
        getWorldSaves();
    }

    public void MainMenuButton()
    {
        CloseAll();
        MainMenu.SetActive(true);
    }

    public void NewGameButton()
    {
        CloseAll();
        NewGameMenu.SetActive(true);
    }

    public void CreateNewGame()
    {
        if (checkIfValid(newGameName.text))
        {
            FileManager.newGame(newGameName.text, getLastPlayed(DateTime.Now));
            MainMenuButton();
        }
    }

    bool checkIfValid(string name)
    {
        if (name == "")
        {
            return false;
        }
        List<WorldData> worlds = FileManager.getSaves();
        foreach (WorldData data in worlds)
        {
            if (data.name.Equals(name))
                return false;
        }
        return true;
    }

    string getLastPlayed(DateTime dt)
    {
        string month;
        switch (dt.Month)
        {
            case 1:
                month = "January";
                break;
            case 2:
                month = "February";
                break;
            case 3:
                month = "March";
                break;
            case 4:
                month = "April";
                break;
            case 5:
                month = "May";
                break;
            case 6:
                month = "June";
                break;
            case 7:
                month = "July";
                break;
            case 8:
                month = "August";
                break;
            case 9:
                month = "September";
                break;
            case 10:
                month = "October";
                break;
            case 11:
                month = "November";
                break;
            case 12:
                month = "December";
                break;
            default:
                month = "January";
                break;
        }

        string day;
        switch (dt.Day)
        {
            case 1:
                day = "1st";
                break;
            case 2:
                day = "2nd";
                break;
            default:
                day = dt.Day.ToString() + "th";
                break;
        }

        int hour;
        string ampm;

        if (dt.Hour > 12) 
        {
            hour = dt.Hour - 12;
            ampm = "PM";
        }
        else
        {
            hour = dt.Hour;
            ampm = "AM";
        }

        return month + " " + day + ", " + hour + ":" + dt.Minute + " " + ampm;
    }
}
