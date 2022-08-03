using System.IO;
using UnityEngine;

public static class SaveSystem
{

    private const string fileLocation = "/savedata.json";

    public static void save()
    {
        string path = Application.persistentDataPath + fileLocation;
        string json = JsonUtility.ToJson(SaveData.Instance);
        Debug.Log("SAVE JSON: " + json);

        using(StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(json);
        }
    }

    public static void load()
    {
        string path = Application.persistentDataPath + fileLocation;
        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            Debug.Log("LOAD JSON: " + json);
            if (json == null || json == string.Empty)
            {
                Debug.Log("Corrupt Save file Detected.");
                sr.Close();
                createEmptySave();
            }
            else
            {
                SaveData.Instance = JsonUtility.FromJson<SaveData>(json);
                sr.Close();
            }
        }
        else
        {
            createEmptySave();
        }

    }


    private static void createEmptySave()
    {
        string path = Application.persistentDataPath + fileLocation;
        SaveData.Instance = new SaveData();
        SaveData.Instance.resolutionWidth = 1280;
        SaveData.Instance.resolutionHeight = 720;
        SaveData.Instance.masterVolume = 1f;
        SaveData.Instance.sfxVolume = 1f;
        SaveData.Instance.musicVolume = 1f;
        SaveData.Instance.mouseSens = 1f;
        SaveData.Instance.bestTime = new float[18];

        string json = JsonUtility.ToJson(SaveData.Instance);
        Debug.Log("Create NEW JSON: " + json);

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(json);
        }

    }

    public static string encodeBase64(string plainString)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainString);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string decodeBase64(string encodedString)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(encodedString);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
