using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public delegate void PlayerAction();

    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip meleeSound;
    /*[Header("Initial parameters")]*/
    private float speed = 2000f;
    private float health = 100f;
    private float maxHealth = 100f;
    private float damage = 5f;
    private Rigidbody2D rb;
    private Animator animator;
	private SpriteRenderer sr;
    private float cooldown = 0f;
    private const float meleeCooldown = 0.5f;
    private int killCount = 0;
    private int level = 1;
    public event PlayerAction MeleeAttack;
    public event PlayerAction LevelUp;
    public float meleeRange = 2f;
	private bool isLeft = true;
	private bool swinging = false;
	private bool swingDirLimiter = false;
	private AudioSource audiosource;

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
		audiosource.clip = hurtSound;
		audiosource.volume = 2f;
		audiosource.Play();
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
		sr = GetComponent<SpriteRenderer>();
		audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
		
		if (cooldown <= 0f)
		{
			animator.SetBool("Attacking", false);
			swinging = false;
			swingDirLimiter = false;
		}
		
		if (Input.GetMouseButton(0) && cooldown <= -0.5f)
        {
            animator.SetBool("Attacking", true);
            cooldown = meleeCooldown;
			swinging = true;
			MeleeAttack();
        }
		
		if (!swinging)
			rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime));
		
		if (!swinging && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Player attack")
		{
			if (rb.velocity.x < 0f)
				isLeft = true;
			else if (rb.velocity.x > 0f)
				isLeft = false;
		}
		
		if (swinging && !swingDirLimiter)
		{
			Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenPosition);
			
			if (mousePos.x > transform.position.x)
				isLeft = false;
			else if (mousePos.x < transform.position.x)
				isLeft = true;
			
			swingDirLimiter = true;
		}
		


		sr.flipX = isLeft;
    }
}
