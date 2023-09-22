using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [Header("Initial parameters")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float maxHealth = 100f;
    private Rigidbody2D rb;
    private float cooldown = 0f;
    private const float meleeCooldown = 1f;
    private int killCount = 0;
    private int level = 1;

    public float Health
    {
        get
        {
            return health;
        }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public int KillCount
    {
        get
        {
            return killCount;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
    }

    public void TellDamage(float hp)
    {
        health -= hp;
    }

    public void TellEnemyKilled()
    {
        killCount++;
        if (killCount > gameManager.RequiredToLevelUp)
        {
            level++;
            killCount = 0;
        }
        // boost as kill reward
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (cooldown > 0f)
            cooldown -= Time.deltaTime;

        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime));

        if (Input.GetMouseButton(0))
        {
            if (cooldown <= 0f)
            {
                gameManager.TellPlayerMeleeAttack();
                cooldown = meleeCooldown;
            }
        }
    }
}
