using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button tutorialButton;
    public Button practiceButton;
    public Button playButton;
    public Button creditsButton;
    public Button leaderboardButton;
    public Button exitButton;
    public string tutorialSceneName;
    public string practiceSceneName;
    public string playSceneName;
    public string creditsSceneName;
    public string leaderboardSceneName;

    private void Start()
    {
        tutorialButton.GetComponent<Button>().onClick.AddListener(() => OpenScene(tutorialSceneName));
        practiceButton.GetComponent<Button>().onClick.AddListener(() => OpenScene(practiceSceneName));
        playButton.GetComponent<Button>().onClick.AddListener(() => OpenScene(playSceneName));
        creditsButton.GetComponent<Button>().onClick.AddListener(() => OpenScene(creditsSceneName));
        leaderboardButton.GetComponent<Button>().onClick.AddListener(() => OpenScene(leaderboardSceneName));
        exitButton.GetComponent<Button>().onClick.AddListener(Application.Quit);
    }
    
    private static void OpenScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
