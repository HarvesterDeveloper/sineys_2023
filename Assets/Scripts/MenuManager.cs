using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject front;
	[SerializeField] private GameObject settingsPanel;
    private float lifeTime = 0f;

	public void LaunchGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
	public void OnOpenSettings()
	{
		settingsPanel.SetActive(true);
	}
	
	public void OnCloseSettings()
	{
		settingsPanel.SetActive(false);
	}

    private void Update()
    {
        lifeTime += Time.deltaTime;

        background.transform.position = new Vector2(Mathf.Cos(lifeTime) * 40f, Mathf.Sin(lifeTime) * 20f);
        front.transform.position = new Vector2(-Mathf.Cos(lifeTime) * 10f, Mathf.Sin(lifeTime) * 5f);
    }


}
