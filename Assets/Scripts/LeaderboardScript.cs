using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.SqliteClient;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScript : MonoBehaviour
{
    public List<Button> modeButtons = new List<Button>();
    public Button resetButton;
    public GameObject scoreTemplate;
    public GameObject leaderboardContent;
    private int _selectedMode;
    private string _dbName;
    private string _dbLocation;
    private void Start()
    {
        _dbName = "Database";
        string directoryPath = $"{Application.dataPath}/Databases";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        _dbLocation = $"URI=file:{Application.dataPath}/Databases/{_dbName}.db";
        _selectedMode = PlayerPrefs.GetInt("Mode");
        foreach (var button in modeButtons)
        {
            var i = modeButtons.IndexOf(button);
            button.onClick.AddListener(() => SelectMode(i));
        }
        resetButton.onClick.AddListener(ResetDatabase);
        SelectMode(_selectedMode);
    }

    private void SelectMode(int id)
    {
        _selectedMode = id;
        foreach (var button in modeButtons)
        {
            button.interactable = true;
        }
        modeButtons[id].interactable = false;
        UpdateLeaderboard();
    }
    private string GetModeName(int id)
    {
        return id switch
        {
            0 => "Quiz",
            1 => "Open",
            2 => "What is correct?",
            3 => "Binary",
            _ => "Unknown"
        };
    }
    private void UpdateLeaderboard()
    {
        foreach (Transform child in leaderboardContent.transform){
            if (child.name != "Score Template") Destroy(child.gameObject);
        }
        ReadFromDatabase(GetModeName(_selectedMode));
    }

    private void ReadFromDatabase(string mode)
    {
        
        IDbConnection db = new SqliteConnection(_dbLocation);
        db.Open();
        var dbcmd = db.CreateCommand();
        var sqlQuery = $"SELECT ROW_NUMBER() OVER(ORDER BY score DESC, time ASC) as row_number, score, time FROM Scores WHERE mode = '{mode}' ORDER BY score DESC, time ASC";
        dbcmd.CommandText = sqlQuery;
        var reader = dbcmd.ExecuteReader();
        var elements = 0;
        while (reader.Read())
        {
            var rowNumber = reader.GetInt32(0);
            var score = reader.GetInt32(1);
            var time = reader.GetFloat(2);
            var minutes = (int)time / 60;
            var seconds = (int)time % 60;
            var milliseconds = (int)(time * 1000) % 1000;
            var timeString = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
            var scoreObject = Instantiate(scoreTemplate, leaderboardContent.transform);
            scoreObject.SetActive(true);
            LeaderboardScoreScript leaderboardScoreScript = scoreObject.GetComponent<LeaderboardScoreScript>();
            leaderboardScoreScript.SetScore(score.ToString());
            leaderboardScoreScript.SetTime(timeString);
            scoreObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,  -100 * (rowNumber));
            elements = rowNumber;
        }
        leaderboardContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 100 * (elements + 1));
    }

    private void ResetDatabase()
    {
        IDbConnection db = new SqliteConnection(_dbLocation);
        db.Open();
        var dbcmd = db.CreateCommand();
        var sqlQuery = "DELETE FROM Scores";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        db.Close();
        UpdateLeaderboard();
    }
}
