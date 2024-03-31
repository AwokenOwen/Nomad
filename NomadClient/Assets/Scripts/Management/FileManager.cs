using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileManager
{
    static string path = Application.persistentDataPath;

    public static List<WorldData> getSaves()
    {
        List<WorldData> saves = new List<WorldData>();

        if (!Directory.Exists(path + "/saves"))
        {
            Directory.CreateDirectory(path + "/saves");
        }

        DirectoryInfo d = new DirectoryInfo(path + "/saves");
        foreach (var file in d.GetFiles("*.sav"))
        {
            WorldData data = JsonUtility.FromJson<WorldData>(File.ReadAllText(path + "/saves/" + file.Name));
            saves.Add(data);
        }

        return saves;
    }

    public static WorldData newGame(string name, string lastPlayed)
    {
        WorldData game = new WorldData(name, lastPlayed);

        string data = JsonUtility.ToJson(game);

        File.WriteAllText(path + "/saves/" + name + ".sav", data);

        return game;
    }

    public static bool checkIfValidWorldName(string name)
    {
        if (name == "")
        {
            return false;
        }
        List<WorldData> worlds = getSaves();
        foreach (WorldData data in worlds)
        {
            if (data.name.Equals(name))
                return false;
        }
        return true;
    }

    public static string getLastPlayed(DateTime dt)
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

    public static void DeleteSave(string name)
    {
        string savePath = path + "/saves/" + name + ".sav";

        File.Delete(savePath);
    }
}
