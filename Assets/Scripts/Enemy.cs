using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float speed = 50f;
    [SerializeField] protected GameManager gameManager;
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

    public void TellDamage(float val)
    {
        // check for buffs, debuffs, defense, etc
        health -= val;
        if (health < 0)
            health = 0;
    }
}
