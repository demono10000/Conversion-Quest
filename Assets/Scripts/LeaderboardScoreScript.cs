using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardScoreScript : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _timeText;
    private void Awake()
    {
        _scoreText = transform.Find("Score Text").GetComponent<TextMeshProUGUI>();
        _timeText = transform.Find("Time Text").GetComponent<TextMeshProUGUI>();
    }
    public void SetScore(string score) => _scoreText.text = score;
    public void SetTime(string time) => _timeText.text = time;
}
