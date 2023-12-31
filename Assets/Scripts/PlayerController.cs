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
    [SerializeField] private AudioClip hitSomeoneSound;
    [SerializeField] private GameObject playeBodyPrefab;
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
	public event PlayerAction Died;

    public float Health
    {
        get
        {
            return health;
        }
		
		set
        {
           if (value < 0f)
                value = 0f;
           
          health = value;
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
		
		set
        {
           if (value < 1f)
                value = 1f;
           
          damage = value;
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
		/*audiosource.clip = hurtSound;
		audiosource.volume = PlayerPrefs.GetFloat("Volume", 1f);
		audiosource.Play();*/
		audiosource.PlayOneShot(hurtSound, PlayerPrefs.GetFloat("volume", 1f));
		
		if (health <= 0)
		{
			Destroy(this.gameObject);
			Instantiate(playeBodyPrefab, transform.position, Quaternion.identity);
			Died();
		}
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
	
	public void OnSwingAnimationEnd()
	{
		audiosource.PlayOneShot(meleeSound, PlayerPrefs.GetFloat("volume", 1f));
		/*audiosource.clip = meleeSound;
		audiosource.volume = PlayerPrefs.GetFloat("Volume", 1f);
		audiosource.Play();*/
		MeleeAttack();
	}
	
	private void OnHitSomeone()
	{
		/*audiosource.clip = hitSomeoneSound;
		audiosource.volume = PlayerPrefs.GetFloat("Volume", 1f);
		audiosource.Play();*/
		audiosource.PlayOneShot(hitSomeoneSound, PlayerPrefs.GetFloat("volume", 1f));
	}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		audiosource = GetComponent<AudioSource>();
		gameManager.PlayerHitSomeone += OnHitSomeone;
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
        }
		
		if (!swinging)
			rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime));
		
		if (rb.velocity.x > 0.5f  || rb.velocity.y > 0.5f || rb.velocity.x < -0.5f || rb.velocity.y < -0.5f)
		{
			animator.SetBool("Walking", true);
		}
		else
		{
			animator.SetBool("Walking", false);
		}
		
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
