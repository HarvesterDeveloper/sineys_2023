using UnityEngine;

public class Simple : Enemy
{
    private float cooldown = 0f;
    private const float attackCooldown = 3f;
    public event EnemyAction SimpleAttack;

    protected override void Start()
    {
		base.Start();
		health = 10f;
        speed = 1000f;
        damage = 10f;
    }

    protected override void Update()
    {
		base.Update();
		
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > 1f)
            {
                float angle = GameManager.DegAngleRelative(transform.position, target.transform.position);
				angle *= Mathf.Deg2Rad;
                rb.AddForce(new Vector2(-Mathf.Cos(angle) * speed * Time.deltaTime, -Mathf.Sin(angle) * speed * Time.deltaTime));
            }
            else
            {
                if (cooldown <= 0f)
                {
                    SimpleAttack(this);
                    cooldown = attackCooldown;
                }
                else
                {
                    cooldown -= Time.deltaTime;
                }
            }
        }
    }
}
