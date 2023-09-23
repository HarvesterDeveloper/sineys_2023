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
    private int requiredToLevelUp = 10;
	private int levelUpIncrease = 5;
    private int requiredToComplete = 5; // lvls to complete mission
    private float lastSpawnTime = 0f;
	private float maxSpawnCooldown = 3f;
	private float spawnCooldown = 3f;

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

        return angle;
    }

	public void OnMeleeRangeUpgrade()
	{
		playerController.MeleeRange += 1f;
        Time.timeScale = 1f;
	}
	
	public void OnMenuOpened()
	{
		Time.timeScale = 0f;
	}
	
	public void OnMenuClosed()
	{
		Time.timeScale = 1f;
	}

    private void OnSimpleEnemyAttack(Enemy initiator)
    {
        if (Vector2.Distance(initiator.transform.position, playerController.gameObject.transform.position) < 1f)
        {
            playerController.TellDamage(initiator.Damage * (initiator.Health / initiator.MaxHealth));
        }
    }
    private void OnPlayerMeleeAttack()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenPosition);

        float angle = DegAngleRelative(playerController.transform.position, mousePos);
		angle *= Mathf.Deg2Rad;
		
		RaycastHit2D hit = Physics2D.Raycast(playerController.transform.position, new Vector2(-Mathf.Cos(angle), -Mathf.Sin(angle)), playerController.meleeRange, LayerMask.GetMask("Enemies"));
		Collider2D col = hit.collider;
		if (col != null)
		{
			Enemy temp = null;
			col.gameObject.TryGetComponent<Enemy>(out temp);
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
		requiredToLevelUp = requiredToLevelUp += levelUpIncrease;
		spawnCooldown = maxSpawnCooldown - ((float)playerController.Level / (float)requiredToComplete * maxSpawnCooldown) / 0.9f;
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

        if (lastSpawnTime > spawnCooldown)
        {
            GameObject spawned =  Instantiate(simpleEnemyprefab, new Vector3(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10), 0), Quaternion.identity);
			spawned.name = "Simple";
            Simple e = spawned.GetComponent<Simple>();
            e.target = playerController.gameObject;
            e.Died += OnEnemyKilled;
            e.Died += playerController.OnEnemyKilled;
            e.SimpleAttack += OnSimpleEnemyAttack;
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
