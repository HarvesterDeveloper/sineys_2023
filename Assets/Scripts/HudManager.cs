using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [Header("HUD Elements")]
    [SerializeField] private Image healthImage;
    [SerializeField] private Image progressImage;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Image fader;
    private bool fadeLimiter = false;
    private float fadePower = 0.1f;
    private float timeSinceStart = 0f;

    private void Update()
    {
        timeSinceStart += Time.deltaTime;
        
        if (!fadeLimiter)
        {
            if (timeSinceStart < 2.5f)
            {
                Color temp = fader.color;
                temp.a -= fadePower * Time.deltaTime;
                fader.color = temp;
                fadePower += 0.25f * Time.deltaTime;
            }
            else
            {
                Destroy(fader.gameObject);
                fadeLimiter = true;
            }
        }


        healthImage.fillAmount = gameManager.Player.Health / gameManager.Player.MaxHealth;
        progressImage.fillAmount = (float) gameManager.Player.KillCount / (float) gameManager.RequiredToLevelUp;
        progressText.text = "Прогресс: " + gameManager.Player.KillCount + "/" + gameManager.RequiredToLevelUp
            + ". Уровень: " + gameManager.Player.Level + "/" + gameManager.RequiredToComplete;
    }
}
