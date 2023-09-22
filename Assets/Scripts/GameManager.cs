using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private int enemiesDied = 0;
    [SerializeField] private int requiredToKill = 10;

    public PlayerController Player
    {
        get
        {
            return playerController;
        }
    }

    public int EnemiesDied
    {
        get
        {
            return enemiesDied;
        }
    }

    public int RequiredToKill
    {
        get
        {
            return requiredToKill;
        }
    }

}
