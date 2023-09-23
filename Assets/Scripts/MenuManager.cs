using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject front;
	[SerializeField] private GameObject settingsPanel;
	[SerializeField] private Slider volumeSlider;
	[SerializeField] private TMP_Text playButtonText;
	[SerializeField] private AudioSource audiosrc;
    private float lifeTime = 0f;
	private int level = 1;

	public void LaunchGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
	public void OnOpenSettings()
	{
		settingsPanel.SetActive(true);
		volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
	}
	
	public void OnCloseSettings()
	{
		settingsPanel.SetActive(false);
	}
	
	public void OnVolumeChanged()
	{
		PlayerPrefs.SetFloat("volume", volumeSlider.value);
		PlayerPrefs.Save();
	}
	
	private void Start()
	{
		level = PlayerPrefs.GetInt("level", 1);
		audiosrc = GetComponent<AudioSource>();
		
	}

    private void Update()
    {
        lifeTime += Time.deltaTime;

        background.transform.position = new Vector2(Mathf.Cos(lifeTime) * 40f, Mathf.Sin(lifeTime) * 20f);
        front.transform.position = new Vector2(-Mathf.Cos(lifeTime) * 10f, Mathf.Sin(lifeTime) * 5f);
		
		if (level == 1)
			playButtonText.text = "Новая игра";
		else
			playButtonText.text = "Продолжить";
		
		audiosrc.volume = PlayerPrefs.GetFloat("volume", 1f);
    }


}
