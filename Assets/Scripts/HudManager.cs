using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [Header("HUD Elements")]
    [SerializeField] private Image healthImage;

    private void Update()
    {
        healthImage.fillAmount = gameManager.Player.Health / gameManager.Player.MaxHealth;
    }
}
