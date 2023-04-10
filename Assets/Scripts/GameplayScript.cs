using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameplayScript : MonoBehaviour
{
    private int _fromSystem;
    private int _toSystem;
    private int _mode;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI numberText;
    public Button confirmButton;
    public List<GameObject> modes = new List<GameObject>();
    public List<Button> quizModeButtons = new List<Button>();
    private string _correctAnswer;
    private string _answer;
    private int _timeStart;
    private int _level;
    private const int Levels = 5;
    private int _correctAnswers;
    // Start is called before the first frame update
    private void Start()
    {
        _fromSystem = PlayerPrefs.GetInt("FromSystem");
        _toSystem = PlayerPrefs.GetInt("ToSystem");
        _mode = PlayerPrefs.GetInt("Mode");
        modes[_mode].SetActive(true);
        foreach (var button in quizModeButtons)
        {
            var i = quizModeButtons.IndexOf(button);
            button.onClick.AddListener(() => QuizAnswer(i));
        }
        confirmButton.onClick.AddListener(() => ConfirmAnswer());
        _timeStart = (int)Time.time;
        _level = 1;
        _correctAnswers = 0;
        GenerateAnswers();
    }

    private void FixedUpdate()
    {
        // show time in format 00:00
        var time = (int)Time.time - _timeStart;
        var minutes = time / 60;
        var seconds = time % 60;
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void QuizAnswer(int i)
    {
        _answer = quizModeButtons[i].GetComponentInChildren<TextMeshProUGUI>().text;
        foreach (var button in quizModeButtons)
        {
            button.interactable = true;
        }
        quizModeButtons[i].interactable = false;
        confirmButton.interactable = true;
    }
    private static string ConvertNumber(int number, int system)
    {
        return system switch
        {
            (int) NumberSystem.Binary => Convert.ToString(number, 2),
            (int) NumberSystem.Octal => Convert.ToString(number, 8),
            (int) NumberSystem.Decimal => Convert.ToString(number, 10),
            (int) NumberSystem.Hexadecimal => Convert.ToString(number, 16),
            _ => throw new ArgumentOutOfRangeException(nameof(system), system, null)
        };
    }

    private void ConfirmAnswer()
    {
        if (_answer == _correctAnswer)
        {
            Debug.Log("Correct");
            _correctAnswers++;
        }
        else
        {
            Debug.Log("Wrong");
        }
        switch (_mode)
        {
            case (int) Mode.Quiz:
                // color correct answer green and wrong answers red
                foreach (var button in quizModeButtons)
                {
                    var text = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (text.text == _correctAnswer)
                    {
                        text.color = Color.green;
                    }
                    else
                    {
                        text.color = Color.red;
                    }
                    button.interactable = false;
                    confirmButton.interactable = false;
                }
                break;
        }
        
        StartCoroutine(WaitAndGenerateAnswers());
    }
    private IEnumerator WaitAndGenerateAnswers()
    {
        yield return new WaitForSeconds(1);
        if (++_level > Levels)
        {
            Debug.Log("Game Over");
            Debug.Log($"Correct answers: {_correctAnswers}");
            Debug.Log($"Time: {timerText.text}");
            PlayerPrefs.SetString("Score", $"{_correctAnswers}/{Levels}");
            PlayerPrefs.SetFloat("Time", Time.time - _timeStart);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ScoreScene");
        }else GenerateAnswers();
    }
    private void GenerateAnswers()
    {
        levelText.text = $"{_level}/{Levels}";
        var number = Random.Range(0, 256);
        numberText.text = ConvertNumber(number, _fromSystem);
        switch (_mode)
        {
            case (int) Mode.Quiz:
                GenerateQuizAnswers(number);
                break;
        }
    }
    private void GenerateQuizAnswers(int number)
    {
        confirmButton.interactable = false;
        foreach (var button in quizModeButtons)
        {
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        _correctAnswer = ConvertNumber(number, _toSystem);
        List<string> answers = new List<string>();
        answers.Add(_correctAnswer);
        for (var i = 0; i < 3; i++)
        {
            var wrongAnswer = Random.Range(0, 256);
            while (answers.Contains(ConvertNumber(wrongAnswer, _toSystem)))
            {
                wrongAnswer = Random.Range(0, 256);
            }
            answers.Add(ConvertNumber(wrongAnswer, _toSystem));
        }
        List<string> shuffledAnswers = answers.OrderBy(x => Random.value).ToList();
        for (var i = 0; i < 4; i++)
        {
            quizModeButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = shuffledAnswers[i];
        }
    }
}
