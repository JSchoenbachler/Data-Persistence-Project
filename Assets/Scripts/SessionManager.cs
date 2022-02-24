using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SessionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SessionManager Instance;
    public string playerName;
    public int score;
    public List<HighScore> highScores;
    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        highScores = new List<HighScore>();
        LoadHighScores();
    }
    public void AddHighScore(HighScore data) {
        if (highScores.Capacity < 5) {
            highScores.Add(data);
        } else {
            for(int i = 0; i < highScores.Capacity; i++) {
                if (highScores[i].score < data.score) {
                    highScores.Insert(i, data);
                    break;
                }
            }
            if (highScores.Capacity > 5) {
                highScores.RemoveAt(5);
            }
        }
        SaveHighScores();
    }
    public void SaveHighScores() {
        
        SaveData saveData = new SaveData();
        saveData.highScores = highScores;
        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    [System.Serializable]
    public class SaveData {
        public List<HighScore> highScores;
    }
    public void LoadHighScores() {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScores = data.highScores;
        }
    }
}
