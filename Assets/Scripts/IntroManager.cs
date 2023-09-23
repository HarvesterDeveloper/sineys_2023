using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	[SerializeField] private TMP_Text textTell;
	private int wayToLevel = 1;
	private int paragraph = 1;
	
	public void NextParagraph()
	{
		paragraph++;
	}
	
	private void Start()
	{
		wayToLevel = PlayerPrefs.GetInt("level", 1);
	}
	
	private void Update()
	{
		if (wayToLevel == 1)
		{
			switch (paragraph)
			{
				case 1:
					textTell.text = "Герой: Эй, что за?";
					break;
				case 2:
					textTell.text = "Герой: Я же просто сидел дома! Кормил кота с ложечки и поливал розочки на подоконнике.";
					break;
				case 3:
					textTell.text = "Злодей: Слушай, приятель. Ты сделал достаточно плохих дел чтобы получить испытание.";
					break;
				case 4:
					textTell.text = "Злодей: Пройдешь их все и я отпущу тебя.";
					break;
				case 5:
					textTell.text = "Герой: Но ведь я...";
					break;
				case 6:
					textTell.text = "Не успев договорить, вы очутились в новом месте.";
					break;
				case 7:
					SceneManager.LoadScene("MissionOne");
					break;
			}
		}
		else if (wayToLevel == 2)
		{
			textTell.text = "2222";
		}
	}
}
