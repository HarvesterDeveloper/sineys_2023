using System.Collections;
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

    public void EnemyTellAttack(Enemy initiator)
    {
        if (Vector2.Distance(initiator.transform.position, playerController.gameObject.transform.position) < 1f)
        {
            playerController.TellDamage(initiator.Damage * (initiator.Health / initiator.MaxHealth));
            Debug.Log(initiator.Damage * (initiator.Health / initiator.MaxHealth));
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
            //enemiesDied++; // its just for debug
        }
    }

}
