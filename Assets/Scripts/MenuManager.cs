using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject front;
    private float lifeTime = 0f;

    private void Update()
    {
        lifeTime += Time.deltaTime;

        background.transform.position = new Vector2(Mathf.Cos(lifeTime) * 40f, Mathf.Sin(lifeTime) * 20f);
        front.transform.position = new Vector2(-Mathf.Cos(lifeTime) * 10f, Mathf.Sin(lifeTime) * 5f);
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
