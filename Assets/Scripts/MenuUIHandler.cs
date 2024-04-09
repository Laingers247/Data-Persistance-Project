using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif



public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public string PlayerName;
    public string CurrentPlayerName;
    public int BestScore;
    SaveData data;

    public void StartNew()
    {
        UpdateData();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }

    public void GetPlayerName()
    {
        if (inputField != null)
        {
            CurrentPlayerName = inputField.text;
            Debug.Log("Player Name: " + CurrentPlayerName);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string CurrentPlayerName;
        public string PlayerName;
        public int BestScore;
    }

    public void UpdateData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log("Get here 1");
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
            
            PlayerName = data.PlayerName;
            BestScore = data.BestScore;
        }
        else
        {
            data = new SaveData();
            data.CurrentPlayerName = CurrentPlayerName;
            data.PlayerName = "None Recorded";
            data.BestScore = 0;
            PlayerName = data.PlayerName;
            BestScore = data.BestScore;
        }

        data.CurrentPlayerName = CurrentPlayerName;
        data.PlayerName = PlayerName;
        data.BestScore = BestScore;

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }


}
