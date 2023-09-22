using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerAction();

    public enum Mission
    {
        LEVEL_ONE,
        LEVEL_TWO,
        LEVEL_THREE
    }

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Mission mission = Mission.LEVEL_ONE;
    [SerializeField] private GameObject simpleEnemyprefab;
    private int requiredToLevelUp = 10; // initial for first level up
    private int requiredToComplete = 5; // lvls to complete mission
    private float lastSpawnTime = 0f;
    public event GameManagerAction AnyUpgradeChoosed;

    public PlayerController Player
    {
        get
        {
            return playerController;
        }
    }

    public int RequiredToLevelUp
    {
        get
        {
            return requiredToLevelUp;
        }
    }

    public int RequiredToComplete
    {
        get
        {
            return requiredToComplete;
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

    public void OnMeleeRangeUpgrade()
    {
        playerController.MeleeRange += 1f;
        Time.timeScale = 1f;
        AnyUpgradeChoosed();
    }

    private void OnSimpleEnemyAttack(Enemy initiator)
    {
        if (Vector2.Distance(initiator.transform.position, playerController.gameObject.transform.position) < 1f)
        {
            playerController.TellDamage(initiator.Damage * (initiator.Health / initiator.MaxHealth));
            //Debug.Log(initiator.Damage * (initiator.Health / initiator.MaxHealth));
        }
    }
    private void OnPlayerMeleeAttack()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenPosition);

        float angle = DegAngleRelative(playerController.transform.position, mousePos);

        Collider2D[] colliders = Physics2D.OverlapAreaAll(playerController.transform.position + new Vector3(-Mathf.Cos(angle) - playerController.MeleeRange, -Mathf.Sin(angle) + playerController.MeleeRange),
            playerController.transform.position + new Vector3(-Mathf.Cos(angle) + playerController.MeleeRange, -Mathf.Sin(angle) - playerController.MeleeRange));
        foreach (Collider2D collider in colliders)
        {
            Enemy temp = null;
            collider.gameObject.TryGetComponent<Enemy>(out temp);
            if (temp != null)
                temp.TellDamage(playerController.Damage);
        }
    }

    private void OnEnemyKilled(Enemy dead)
    {
        //
    }

    private void OnLevelUp()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        switch (mission)
        {
            case Mission.LEVEL_ONE:
                requiredToLevelUp = 5;
                requiredToComplete = 5;
                break;
            case Mission.LEVEL_TWO:
                requiredToLevelUp = 25;
                requiredToComplete = 7;
                break;
            case Mission.LEVEL_THREE:
                requiredToLevelUp = 33;
                requiredToComplete = 10;
                break;
        }
        StartCoroutine("EverySecond");
        playerController.MeleeAttack += OnPlayerMeleeAttack;
        playerController.LevelUp += OnLevelUp;
    }

    private void Update()
    {
        lastSpawnTime += Time.deltaTime;

        if (lastSpawnTime > 3f)
        {
            GameObject spawned =  Instantiate(simpleEnemyprefab, new Vector3(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10), 0), Quaternion.identity);
            Simple e = spawned.GetComponent<Simple>();
            e.target = playerController.gameObject;
            e.Died += OnEnemyKilled;
            e.Died += playerController.OnEnemyKilled;
            e.SimpleAttack += OnSimpleEnemyAttack;
            e.Init();
            lastSpawnTime = 0f;
        }
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
