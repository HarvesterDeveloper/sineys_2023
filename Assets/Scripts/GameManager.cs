using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        StartCoroutine("EverySecond");
    }

    private IEnumerator EverySecond()
    {
        for (int i = 0; i < 10_000; i++)
        {
            yield return new WaitForSeconds(1);
            enemiesDied++; // its just for debug
        }
    }

}
