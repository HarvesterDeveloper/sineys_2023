using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Initial parameters")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float maxHealth = 100f;

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

    public void TellDamage(float hp)
    {
        health -= hp;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime));
    }
}
