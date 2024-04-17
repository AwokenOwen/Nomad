using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileManager
{
    static string path = Application.persistentDataPath;

    public static List<SaveMetaData> getSaves()
    {
        List<SaveMetaData> saves = new List<SaveMetaData>();

        if (!Directory.Exists(path + "/saves"))
        {
            Directory.CreateDirectory(path + "/saves");
        }

        DirectoryInfo d = new DirectoryInfo(path + "/saves");
        foreach (var file in d.GetFiles("*.msav"))
        {
            string data = File.ReadAllText(file.FullName);

            SaveMetaData meta = JsonUtility.FromJson<SaveMetaData>(data);
            saves.Add(meta);
        }

        return saves;
    }

    public static WorldData loadWorld(string name)
    {
        string data = File.ReadAllText (path + "/saves/" + name + ".sav");

        WorldData world = JsonUtility.FromJson<WorldData>(data);
        UpdateMetaData(name);
        return world;
    }

    public static void saveWorld(WorldData world)
    {
        string data = JsonUtility.ToJson(world, true);

        string metaData = JsonUtility.ToJson(new SaveMetaData(world.name, getLastPlayed(DateTime.Now)), true);

        File.WriteAllText(path + "/saves/" + world.name + ".sav", data);

        File.WriteAllText(path + "/saves/" + world.name + ".msav", metaData);
    }

    public static bool checkIfValidWorldName(string name)
    {
        if (name == "")
        {
            return false;
        }
        List<SaveMetaData> worlds = getSaves();
        foreach (SaveMetaData metaData in worlds)
        {
            if (metaData.name.Equals(name))
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

        string minute = dt.Minute.ToString("00");

        return month + " " + day + ", " + hour + ":" + minute + " " + ampm;
    }

    public static void DeleteSave(string name)
    {
        string savePath = path + "/saves/" + name + ".sav";
        string saveMetaPath = path + "/saves/" + name + ".msav";

        File.Delete(savePath);
        File.Delete(saveMetaPath);
    }
    
    static void UpdateMetaData(string name)
    {
        string metaData = JsonUtility.ToJson(new SaveMetaData(name, getLastPlayed(DateTime.Now)), true);

        File.WriteAllText(path + "/saves/" + name + ".msav", metaData);
    }

    public static void SaveSettings(SettingsData data)
    {
        string settings = JsonUtility.ToJson(data);

        File.WriteAllText(path + "/settings.json", settings);
    }

    public static SettingsData getSettings()
    {
        if (File.Exists(path + "/settings.json"))
        {
            string settings = File.ReadAllText(path + "/settings.json");

            return JsonUtility.FromJson<SettingsData>(settings);
        }

        SaveSettings(new SettingsData());

        return getSettings();
    }
}
