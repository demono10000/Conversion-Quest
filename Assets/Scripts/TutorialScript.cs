using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
	[SerializeField]
	public List<TutorialData> tutorialData = new List<TutorialData>();
	public Button nextButton;
	public Button previousButton;
	public TextMeshProUGUI tutorialText;
	public Image tutorialImage;

	private int _page;
	// Start is called before the first frame update
	private void Start()
	{
		// disable previousButton
		previousButton.gameObject.SetActive(false);
		nextButton.gameObject.SetActive(true);
		var data = tutorialData[0];
		tutorialText.text = data.text;
		tutorialImage.sprite = data.image;
		nextButton.GetComponent<Button>().onClick.AddListener(NextPage);
		previousButton.GetComponent<Button>().onClick.AddListener(PreviousPage);
		_page = 0;
	}

	private void NextPage()
	{
		nextButton.gameObject.SetActive(++_page != tutorialData.Count - 1 );
		previousButton.gameObject.SetActive(true);
		var data = tutorialData[_page];
		tutorialText.text = data.text;
		tutorialImage.sprite = data.image;
	}
	private void PreviousPage()
	{
		previousButton.gameObject.SetActive(--_page != 0);
		nextButton.gameObject.SetActive(true);
		var data = tutorialData[_page];
		tutorialText.text = data.text;
		tutorialImage.sprite = data.image;
	}
}
