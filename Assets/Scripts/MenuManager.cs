using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene("Match"); // Change it to right name
    }
}
