using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum  NumberSystem
{
    Binary,
    Octal,
    Decimal,
    Hexadecimal
}
public enum Mode
{
    Quiz,
    Open,
    WhatIsCorrect,
    Binary
}
public class ModesScript : MonoBehaviour
{
    public List<Button> fromButtons = new List<Button>();
    public List<Button> toButtons = new List<Button>();
    public List<Button> modeButtons = new List<Button>();

    public Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var button in fromButtons)
        {
            var i = fromButtons.IndexOf(button);
            button.onClick.AddListener(() => SelectFromSystem(i));
        }
        foreach (var button in toButtons)
        {
            var i = toButtons.IndexOf(button);
            button.onClick.AddListener(() => SelectToSystem(i));
        }
        foreach (var button in modeButtons)
        {
            var i = modeButtons.IndexOf(button);
            button.onClick.AddListener(() => SelectMode(i));
        }
        playButton.onClick.AddListener(() => SceneManager.LoadScene("GameplayScene"));
        SelectFromSystem((int)NumberSystem.Binary);
        SelectToSystem((int)NumberSystem.Decimal);
        SelectMode((int)Mode.Quiz);
    }
    
    private void SelectFromSystem(int i)
    {
        PlayerPrefs.SetInt("FromSystem", i);
        foreach (var button in fromButtons)
        {
            button.interactable = true;
        }
        foreach (var button in toButtons)
        {
            button.gameObject.SetActive(true);
        }
        fromButtons[i].interactable = false;
        toButtons[i].gameObject.SetActive(false);
    }
    private void SelectToSystem(int i)
    {
        PlayerPrefs.SetInt("ToSystem", i);
        foreach (var button in toButtons)
        {
            button.interactable = true;
        }
        foreach (var button in fromButtons)
        {
            button.gameObject.SetActive(true);
        }
        toButtons[i].interactable = false;
        fromButtons[i].gameObject.SetActive(false);
    }
    private void SelectMode(int i)
    {
        PlayerPrefs.SetInt("Mode", i);
        foreach (var button in modeButtons)
        {
            button.interactable = true;
        }
        modeButtons[i].interactable = false;
    }
}
