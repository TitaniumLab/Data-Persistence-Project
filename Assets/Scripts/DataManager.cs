using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    //default settings
    public string playerName = "Player";
    public int gameSpeed = 1;
    //a list storing data about the top players between scenes
    public List<(string names, int score)> namesAndScore = new List<(string names, int score)>();

    private void Awake()
    {
        //singletone
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //loading previous session data
        _LoadData();
        //applying game speed
        GameSpeedScale();
    }

    //change game speed
    private void GameSpeedScale()
    {
        switch (gameSpeed)
        {
            case 0: Time.timeScale = 0.75f; break;
            case 1: Time.timeScale = 1.0f; break;
            case 2: Time.timeScale = 1.5f; break;
        }
        Debug.Log($"TimeScaled {Time.timeScale}");
    }

    //class for data saving
    [Serializable]
    private class SaveData
    {
        public string playerName;
        public int gameSpeed;
        public string[] scoreNamesList = new string[10];
        public int[] scoreList = new int[10];
    }

    public void _SaveData()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.gameSpeed = gameSpeed;
        //saving only top 10 players
        for (int i = 0; i < 10; i++)
        {
            if (i < namesAndScore.Count)
            {
                data.scoreNamesList[i] = namesAndScore[i].names;
                data.scoreList[i] = namesAndScore[i].score;
            }
        }
        //applying game speed when saving in the settings menu
        GameSpeedScale();
        //saving
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log($"Setings saved. ({Application.persistentDataPath}/savefile.json)");
    }

    public void _LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            gameSpeed = data.gameSpeed;
            for (int i = 0; i < data.playerName.Length; i++)
            {
                if (data.scoreNamesList[i] != null)
                {
                    namesAndScore.Add((data.scoreNamesList[i], data.scoreList[i]));
                }
            }
            Debug.Log($"Settings loaded. ({Application.persistentDataPath}/savefile.json)");
        }
    }
}
