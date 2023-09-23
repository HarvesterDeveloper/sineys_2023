using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    public delegate void EnemyAction(Enemy caller);

    protected float health = 100f;
    protected float maxHealth = 100f;
    protected float speed = 50f;
    protected float damage = 5f;
    [HideInInspector] public GameObject target;
    protected Rigidbody2D rb;
	protected bool isLeft = true;
	protected SpriteRenderer sr;
    public event EnemyAction Died;

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

    public float Damage
    {
        get
        {
            return damage;
        }
    }

    public void TellDamage(float val)
    {
        // check for buffs, debuffs, defense, etc
        health -= val;
        if (health <= 0)
        {
            Died(this);
            Destroy(this.gameObject);
        }
            
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
    }
	
	protected virtual void Update()
	{
		if (rb.velocity.x < 0f)
			isLeft = true;
		else if (rb.velocity.x > 0f)
			isLeft = false;
		
		sr.flipX = !isLeft;
	}
}
