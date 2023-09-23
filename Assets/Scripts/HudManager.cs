using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
	public delegate void HudAction();
	
    [SerializeField] private GameManager gameManager;
    [Header("HUD Elements")]
    [SerializeField] private Image healthImage;
    [SerializeField] private Image progressImage;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Image fader;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject menu;
	[SerializeField] private Slider volumeSlider;
    private float fadePower = 0.1f;
	private bool winFade = false;
	private float winFadeTime = 0f;
    private float timeSinceStart = 0f;
	public event HudAction MenuOpened;
	public event HudAction MenuClosed;
	public event HudAction MeleeRangeUpgraded;

    public void OnMeleeRangeUpgrade()
    {
		MeleeRangeUpgraded();
		upgradePanel.SetActive(false);
    }
	
	public void OnExit()
	{
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1f;
	}
	
	public void OnResume()
	{
		MenuClosed();
		menu.SetActive(false);
	}
	
	public void OnMissionComplete()
	{
		upgradePanel.SetActive(false);
		winFade = true;
		fadePower = 0.1f;
	}
	
	public void OnVolumeChanged()
	{
		PlayerPrefs.SetFloat("volume", volumeSlider.value);
		PlayerPrefs.Save();
	}

    private void Start()
    {
        gameManager.Player.LevelUp += OnLevelUp;
		gameManager.MissionComplete += OnMissionComplete;
		MeleeRangeUpgraded += gameManager.OnMeleeRangeUpgrade;
		MenuOpened += gameManager.OnMenuOpened;
		MenuClosed += gameManager.OnMenuClosed;
    }

    private void Update()
    {
        timeSinceStart += Time.deltaTime;
        
		if (timeSinceStart < 2.5f)
        {
            Color temp = fader.color;
            temp.a -= fadePower * Time.deltaTime;
            fader.color = temp;
            fadePower += 0.25f * Time.deltaTime;
        }
		else if (winFade)
		{
			Color temp = fader.color;
            temp.a += fadePower * Time.deltaTime;
            fader.color = temp;
            fadePower += 0.25f * Time.deltaTime;
			
			winFadeTime += Time.deltaTime;
			
			if (winFadeTime >= 3f)
			{
				SceneManager.LoadScene("Menu");// goto next level
			}
		}
		


        healthImage.fillAmount = gameManager.Player.Health / gameManager.Player.MaxHealth;
        progressImage.fillAmount = (float) gameManager.Player.KillCount / (float) gameManager.RequiredToLevelUp;
        progressText.text = "Прогресс: " + gameManager.Player.KillCount + "/" + gameManager.RequiredToLevelUp
            + ". Уровень: " + gameManager.Player.Level + "/" + gameManager.RequiredToComplete;
			
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
			{
				MenuOpened();
				volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
				menu.SetActive(true);
			}
			else
			{
				MenuClosed();
				menu.SetActive(false);
			}
        }
    }

    private void OnLevelUp()
    {
        upgradePanel.SetActive(true);
    }
}
