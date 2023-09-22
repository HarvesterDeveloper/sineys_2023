using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    public PlayerController Player
    {
        get
        {
            return playerController;
        }
    }

}
