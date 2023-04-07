using UnityEngine;
using UnityEngine.UI;

public class ReturnButtonScript : MonoBehaviour
{
    private const string MenuSceneName = "MenuScene";

    void Start()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(GoToMenu);
    }
    
    void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MenuSceneName);
    }
}
