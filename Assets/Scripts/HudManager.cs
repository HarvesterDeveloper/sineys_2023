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

    private void Update()
    {
        healthImage.fillAmount = gameManager.Player.Health / gameManager.Player.MaxHealth;
        progressImage.fillAmount = gameManager.EnemiesDied / gameManager.RequiredToKill;
    }
}
