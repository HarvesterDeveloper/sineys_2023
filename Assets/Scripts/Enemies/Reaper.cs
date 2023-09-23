using UnityEngine;

public class Reaper : Enemy
{
    private float cooldown = 0f;
    private const float attackCooldown = 3f;
    public event EnemyAction ReaperFireball;

    protected override void Start()
    {
		base.Start();
		health = 5f;
        speed = 1000f;
        damage = 20f;
    }

    protected override void Update()
    {
		base.Update();
		
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > 5f)
            {
                float angle = GameManager.DegAngleRelative(transform.position, target.transform.position);
				angle *= Mathf.Deg2Rad;
                rb.AddForce(new Vector2(-Mathf.Cos(angle) * speed * Time.deltaTime, -Mathf.Sin(angle) * speed * Time.deltaTime));
            }
            else
            {
                if (cooldown <= 0f)
                {
                    ReaperFireball(this);
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
