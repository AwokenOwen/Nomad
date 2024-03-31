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

        DirectoryInfo d = new DirectoryInfo(path);
        foreach (var file in d.GetFiles("*.sav"))
        {
            WorldData data = JsonUtility.FromJson<WorldData>(File.ReadAllText(path + "/" + file.Name));
            saves.Add(data);
        }

        return saves;
    }

    public static WorldData newGame(string name, string lastPlayed)
    {
        WorldData game = new WorldData(name, lastPlayed);

        string data = JsonUtility.ToJson(game);

        File.WriteAllText(path + "/" + name + ".sav", data);

        return game;
    }
}
