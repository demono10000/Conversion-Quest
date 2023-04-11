using System.Data;
using System.IO;
using Mono.Data.SqliteClient;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button leaderboardButton;
    public Button returnButton;
    public TextMeshProUGUI savingText;

    private void Start()
    {
        leaderboardButton.onClick.AddListener(() => { SceneManager.LoadScene("LeaderboardScene"); });
        var score = PlayerPrefs.GetString("Score");
        var correctAnswers = int.Parse(score.Split("/")[0]);
        var wrongAnswers = int.Parse(score.Split("/")[1]) - correctAnswers;
        var time = PlayerPrefs.GetFloat("Time");
        var minutes = (int)time / 60;
        var seconds = (int)time % 60;
        var milliseconds = (int)(time * 1000) % 1000;
        var timeString = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
        var textTemplate = text.text;
        textTemplate = textTemplate
            .Replace("{0}", correctAnswers.ToString())
            .Replace("{1}", wrongAnswers.ToString())
            .Replace("{2}", timeString);
        text.text = textTemplate;
        string mode = (PlayerPrefs.GetInt("Mode")) switch
        {
            0 => "Quiz",
            1 => "Open",
            2 => "What is correct?",
            3 => "Binary",
            _ => "Unknown"
        };
        SaveToDatabase(correctAnswers, time, mode);
    }

    private void SaveToDatabase(int score, float time, string mode)
    {
        leaderboardButton.interactable = false;
        returnButton.interactable = false;
        string dbName = "Database";
        string directoryPath = $"{Application.dataPath}/Databases";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        string dbLocation = $"URI=file:{Application.dataPath}/Databases/{dbName}.db";
        IDbConnection db = new SqliteConnection(dbLocation);
        db.Open();
        IDbCommand dbcmd = db.CreateCommand();
        string sqlQuery = "CREATE TABLE IF NOT EXISTS Scores (id INTEGER PRIMARY KEY AUTOINCREMENT, score INTEGER, time FLOAT, mode TEXT)";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        sqlQuery = $"INSERT INTO Scores (score, time, mode) VALUES ({score}, '{time}', '{mode}')";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        db.Close();
        savingText.text = "Score saved";
        leaderboardButton.interactable = true;
        returnButton.interactable = true;
    }
}
