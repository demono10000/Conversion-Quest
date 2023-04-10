using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NumberButtonScript : MonoBehaviour
{
    private bool _state;
    private TextMeshProUGUI _text;
    private Button _button;
    private bool _isCorrect;
    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _button.onClick.AddListener(SwitchState);
    }
    public bool GetState()
    {
        return _state;
    }
    private void SwitchState()
    {
        _state = !_state;
        _text.color = _state ? Color.green : Color.red;
    }
    public void SetValue(string value, bool correct, int system)
    {
        _isCorrect = correct;
        if (correct) _text.text = value;
        else
        {
            // generate rendom number
            var maxValue = system switch
            {
                (int) NumberSystem.Binary => 1,
                (int) NumberSystem.Octal => 7,
                (int) NumberSystem.Decimal => 9,
                (int) NumberSystem.Hexadecimal => 15,
                _ => throw new ArgumentOutOfRangeException(nameof(system), system, null)
            };
            string random = value;
            while (random == value)
            {
                random = Convert.ToString(Random.Range(0, maxValue + 1), 16);
            }
            _text.text = random;
        }
    }
    public string GetValue()
    {
        return _text.text;
    }
    public void SetColor(Color color)
    {
        _text.color = color;
    }
    public void SwitchButtons(bool state)
    {
        _button.interactable = state;
    }

    public void SetState(bool state)
    {   
        _state = state;
        _text.color = _state ? Color.green : Color.red;
    }
    public bool IsCorrect()
    {
        return _isCorrect == _state;
    }
}
