using System;
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

    public static float DegAngleRelative(Vector3 relate, Vector3 wanted)
    {
        double dx = relate.x - wanted.x;
        double dy = relate.y - wanted.y;
        double a2 = Math.Atan2(dy, dx);
        float angle = (float)((180 / Math.PI) * a2);
        if (angle < 0)
            angle = angle + 360;
        angle *= Mathf.Deg2Rad;

        return angle;
    }

    public void TellEnemyAttack(Enemy initiator)
    {
        if (Vector2.Distance(initiator.transform.position, playerController.gameObject.transform.position) < 1f)
        {
            playerController.TellDamage(initiator.Damage * (initiator.Health / initiator.MaxHealth));
            //Debug.Log(initiator.Damage * (initiator.Health / initiator.MaxHealth));
        }
    }

    public void EnemyDied(Enemy dead)
    {
        enemiesDied++;
    }

    public void TellPlayerMeleeAttack()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenPosition);

        float angle = DegAngleRelative(playerController.transform.position, mousePos);

        Collider2D[] colliders = Physics2D.OverlapAreaAll(playerController.transform.position + new Vector3(-Mathf.Cos(angle) - 1f, -Mathf.Sin(angle) + 1f),
            playerController.transform.position + new Vector3(-Mathf.Cos(angle) + 1f, -Mathf.Sin(angle) - 1f));
        foreach (Collider2D collider in colliders)
        {
            Enemy temp = null;
            collider.gameObject.TryGetComponent<Enemy>(out temp);
            if (temp != null)
                temp.TellDamage(25f);
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
