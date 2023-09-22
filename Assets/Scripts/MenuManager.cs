using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    private float lifeTime = 0f;

    private void Update()
    {
        lifeTime += Time.deltaTime;

        background.transform.position = new Vector2(Mathf.Cos(lifeTime) * 20f, Mathf.Sin(lifeTime) * 10f);
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("Match"); // Change it to right name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
