using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PracticeScript : MonoBehaviour
{
    public List<Button> fromButtons = new List<Button>();
    public List<Button> toButtons = new List<Button>();
    public TextMeshProUGUI outputText;
    public List<NumberScript> inputFields = new List<NumberScript>();

    private int _digitsSum;
    // Start is called before the first frame update
    private void Start()
    {
        foreach (var button in fromButtons)
        {
            button.onClick.AddListener(() => SelectFromType(fromButtons.IndexOf(button)));
        }
        foreach (var button in toButtons)
        {
            button.onClick.AddListener(() => SelectToType(toButtons.IndexOf(button)));
        }
        SelectFromType(0);
        SelectToType(0);
        _digitsSum = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // check if digits sum changed
        var sum = 0;
        foreach (var inputField in inputFields)
        {
            sum += inputField.GetNumber();
        }
        if (sum != _digitsSum)
        {
            _digitsSum = sum;
            CalculateOutput();
        }
    }
    
    private void SelectFromType(int type)
    {
        foreach (var button in fromButtons)
        {
            button.interactable = true;
        }
        fromButtons[type].interactable = false;
        ConfigureInputs(type);
    }
    private void SelectToType(int type)
    {
        foreach (var button in toButtons)
        {
            button.interactable = true;
        }
        toButtons[type].interactable = false;
        CalculateOutput();
    }
    private void ConfigureInputs(int type)
    {
        int [] maxNumbers = {1, 7, 9, 15};
        foreach (var inputField in inputFields)
        {
            inputField.SetMaxNumber(maxNumbers[type]);
            inputField.ResetNumber();
        }
    }

    private void CalculateOutput()
    {
        int[] digits = new int[inputFields.Count];
        for (var i = 0; i < inputFields.Count; i++)
        {
            digits[i] = inputFields[i].GetNumber();
        }

        uint inputNumber = 0;

        var power = 0;
        if (!fromButtons[0].interactable)  power = 2;
        else if (!fromButtons[1].interactable) power = 8;
        else if (!fromButtons[2].interactable) power = 10;
        else if (!fromButtons[3].interactable) power = 16;

        for (var i = 0; i < digits.Length; i++)
        {
            inputNumber += (uint)(digits[i] * (int)Mathf.Pow(power, digits.Length - i - 1));
        }

        if (!toButtons[0].interactable) outputText.text = Convert.ToString(inputNumber, 2);
        else if (!toButtons[1].interactable) outputText.text = Convert.ToString(inputNumber, 8);
        else if (!toButtons[2].interactable) outputText.text = inputNumber.ToString();
        else if (!toButtons[3].interactable) outputText.text = inputNumber.ToString("X");
    }
}
