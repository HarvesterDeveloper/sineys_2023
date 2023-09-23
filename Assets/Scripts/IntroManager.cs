using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	[SerializeField] private TMP_Text textTell;
	private int wayToLevel = 1;
	private int paragraph = 1;
	private Color green = new Color(0f, 1f, 0f);
	private Color red = new Color(1f, 0f, 0f);
	private Color white = new Color(1f, 1f, 1f);
	
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
					textTell.color = green;
					break;
				case 2:
					textTell.text = "Герой: Я же просто сидел дома! Кормил кота с ложечки и поливал розы на подоконнике.";
					textTell.color = green;
					break;
				case 3:
					textTell.text = "Злодей: Слушай, приятель. Ты сделал достаточно плохих дел чтобы получить испытание.";
					textTell.color = red;
					break;
				case 4:
					textTell.text = "Злодей: Пройдешь их все и я отпущу тебя.";
					textTell.color = red;
					break;
				case 5:
					textTell.text = "Герой: Но ведь я...";
					textTell.color = green;
					break;
				case 6:
					textTell.text = "Не успев договорить, вы очутились в новом месте.";
					textTell.color = white;
					break;
				case 7:
					SceneManager.LoadScene("MissionOne");
					break;
			}
		}
		else if (wayToLevel == 2)
		{
			switch (paragraph)
			{
				case 1:
					textTell.text = "Герой: Боже. Я только только что был в прошлом!";
					textTell.color = green;
					break;
				case 2:
					textTell.text = "Злодей: Да, смышленышь. Но не радуйся раньше времени, твой путь домой еще не закончен.";
					textTell.color = red;
					break;
				case 3:
					textTell.text = "Злодей: Ты побывал в простых временах. Думаю, тебе стоит показать и другие.";
					textTell.color = red;
					break;
				case 4:
					SceneManager.LoadScene("MissionTwo");
					break;
			}
		}
		else if (wayToLevel == 3)
		{
			switch (paragraph)
			{
				case 1:
					textTell.text = "Злодей: Хммм. Ты лучше, чем я тебя представлял!";
					textTell.color = red;
					break;
				case 2:
					textTell.text = "Злодей: Ладно, победа будет твоей...";
					textTell.color = red;
					break;
				case 3:
					textTell.text = "Злодей: Но при одном условии - ты должен вернуться в самый старинные времена.";
					textTell.color = red;
					break;
				case 4:
					textTell.text = "Герой: Я просто хочу домой.";
					textTell.color = green;
					break;
				case 5:
					SceneManager.LoadScene("MissionThree");
					break;
			}
		}
	}
}
