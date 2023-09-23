using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public delegate void PlayerAction();

    [SerializeField] private GameManager gameManager;
    /*[Header("Initial parameters")]*/
    private float speed = 2000f;
    private float health = 100f;
    private float maxHealth = 100f;
    private float damage = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private float cooldown = 0f;
    private const float meleeCooldown = 1f;
    private int killCount = 0;
    private int level = 1;
    public event PlayerAction MeleeAttack;
    public event PlayerAction LevelUp;
    public float meleeRange = 2f;

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

   public float Damage
   {
        get
        {
            return damage;
        }
   }

   public float MeleeRange
   {
        get
        {
            return meleeRange;
        }

        set
        {
           if (value < 2f)
                value = 2f;
           
          meleeRange = value;
        }
   }

    public void TellDamage(float hp)
    {
        health -= hp;
    }

    public void OnEnemyKilled(Enemy who)
    {
        killCount++;
        if (killCount >= gameManager.RequiredToLevelUp)
        {
            level++;
            killCount = 0;
            LevelUp();
        }
        // boost as kill reward
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
                animator.SetBool("Attacking", true);
                MeleeAttack();
                cooldown = meleeCooldown;
            }
            else
            {
                animator.SetBool("Attacking", false);
            }
        }
        else
        {
            //
        }
    }
}
