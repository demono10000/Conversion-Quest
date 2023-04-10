using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button leaderboardButton;

    private void Start()
    {
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
    }
}
