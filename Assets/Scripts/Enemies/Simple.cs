﻿using UnityEngine;

public class Simple : Enemy
{
    private float cooldown = 0f;
    private const float attackCooldown = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 1000f;
        damage = 3f;
    }

    private void Update()
    {
        GameObject plr = gameManager.Player.gameObject;

        float angle = GameManager.DegAngleRelative(transform.position, plr.transform.position);

        if (Vector2.Distance(transform.position, gameManager.Player.transform.position) > 1f)
            rb.AddForce(new Vector2(-Mathf.Cos(angle) * speed * Time.deltaTime, -Mathf.Sin(angle) * speed * Time.deltaTime));

        if (cooldown <= 0f)
        {
            gameManager.TellEnemyAttack(this);
            cooldown = attackCooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}
