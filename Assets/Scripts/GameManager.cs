using System;
using System.Collections;
using System.Collections.Generic;
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
	//private float maxSpawnCooldown = 3f;
	private float spawnCooldown = 3f;
	private int enemiesCount = 0;
	private int maxEnemyCount = 10;
	public event GameManagerAction MissionComplete;
	public event GameManagerAction PlayerHitSomeone;
	private bool missionEnded = false;

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
		Time.timeScale = 1f; // gives a bug while choosing upgrade
	}

    private void OnSimpleEnemyAttack(Enemy initiator)
    {
        if (Vector2.Distance(initiator.transform.position, playerController.gameObject.transform.position) < 2f) // 2f - simple's range
        {
            playerController.TellDamage(initiator.Damage);
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
			{
				temp.TellDamage(playerController.Damage);
				PlayerHitSomeone();
			}
				
		}
    }

    private void OnEnemyKilled(Enemy dead)
    {
        enemiesCount--;
    }

    private void OnLevelUp()
    {
		DestroyAllEnemies();
		if (playerController.Level > requiredToComplete)
		{
			MissionComplete();
			switch (mission)
			{
				case Mission.LEVEL_ONE:
					PlayerPrefs.SetInt("level", 2);
					PlayerPrefs.Save();
					break;
				case Mission.LEVEL_TWO:
					PlayerPrefs.SetInt("level", 3);
					PlayerPrefs.Save();
					break;
				case Mission.LEVEL_THREE:
					PlayerPrefs.SetInt("level", 1);
					PlayerPrefs.Save();
					break;
			}
			missionEnded = true;
		}
		else
		{
			Time.timeScale = 0f;
			requiredToLevelUp = requiredToLevelUp += levelUpIncrease;
			//spawnCooldown = maxSpawnCooldown; // fixme
		}
    }
	
	private void OnPlayerDied()
	{
		DestroyAllEnemies();
		missionEnded = true;
	}

    private void Start()
    {
        switch (mission)
        {
            case Mission.LEVEL_ONE:
                requiredToLevelUp = 5;
                requiredToComplete = 3;
				levelUpIncrease = 1;
				maxEnemyCount = 10;
                break;
            case Mission.LEVEL_TWO:
                requiredToLevelUp = 5;
                requiredToComplete = 4;
				levelUpIncrease = 2;
				maxEnemyCount = 15;
                break;
            case Mission.LEVEL_THREE:
                requiredToLevelUp = 5;
                requiredToComplete = 5;
				levelUpIncrease = 3;
				maxEnemyCount = 25;
                break;
        }
        StartCoroutine("EverySecond");
        playerController.MeleeAttack += OnPlayerMeleeAttack;
        playerController.LevelUp += OnLevelUp;
        playerController.Died += OnPlayerDied;
    }

    private void Update()
    {
        lastSpawnTime += Time.deltaTime;

		if (missionEnded)
			return;

		if (enemiesCount < maxEnemyCount && enemiesCount < 100) //100 - hardcoded just in case
		{
			if (lastSpawnTime > spawnCooldown)
			{
				Debug.Log("lst="+lastSpawnTime+" ec="+enemiesCount+" sc="+spawnCooldown);
				GameObject spawned =  Instantiate(simpleEnemyprefab, new Vector3(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10), 0), Quaternion.identity);
				spawned.name = "Simple";
				Simple e = spawned.GetComponent<Simple>();
				e.target = playerController.gameObject;
				e.Died += OnEnemyKilled;
				e.Died += playerController.OnEnemyKilled;
				e.SimpleAttack += OnSimpleEnemyAttack;
				lastSpawnTime = 0f;
				enemiesCount++;
			}
		}
    }
	
	private void DestroyAllEnemies()
	{
		Enemy[] objects = FindObjectsOfType<Enemy>();

        foreach (Enemy e in objects)
        {
			Destroy(e.gameObject);
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
