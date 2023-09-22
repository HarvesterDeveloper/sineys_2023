using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
    protected float health = 100f;
    protected float maxHealth = 100f;
    protected float speed = 50f;
    protected float damage = 5f;
    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
        if (health < 0)
        {
            gameManager.TellEnemyKilled(this);
            Destroy(this.gameObject);
        }
            
    }
}
