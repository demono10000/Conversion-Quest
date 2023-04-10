using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberScript : MonoBehaviour
{
    private int _maxNumber = 15;
    private int _number;
    private Button _upButton;
    private Button _downButton;
    private TextMeshProUGUI _numberText;

    public void Awake()
    {
        _upButton = transform.Find("Up").GetComponent<Button>();
        _downButton = transform.Find("Down").GetComponent<Button>();
        _numberText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _upButton.onClick.AddListener(AddNumber);
        _downButton.onClick.AddListener(SubtractNumber);
        _number = 0;
    }
    private void AddNumber()
    {
        if (_number >= _maxNumber) return;
        _numberText.text = (++_number).ToString("X");
    }
    private void SubtractNumber()
    {
        if (_number <= 0) return;
        _numberText.text = (--_number).ToString("X");
    }
    public void SetMaxNumber(int maxNumber)
    {
        _maxNumber = maxNumber;
    }
    
    public int GetNumber()
    {
        return _number;
    }
    public void ResetNumber()
    {
        if(_numberText == null) return;
        _number = 0;
        _numberText.text = _number.ToString("X");
    }
    public void SwitchButtons(bool state)
    {
        _upButton.interactable = state;
        _downButton.interactable = state;
    }
}