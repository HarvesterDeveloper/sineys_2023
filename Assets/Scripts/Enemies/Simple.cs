using System;
using UnityEngine;

public class Simple : Enemy
{
    private void Update()
    {
        GameObject plr = gameManager.Player.gameObject;

        double dx = transform.position.x - plr.transform.position.x;
        double dy = transform.position.y - plr.transform.position.y;
        double a2 = Math.Atan2(dy, dx);
        float angle = (float)((180 / Math.PI) * a2);
        if (angle < 0)
            angle = angle + 360;
        angle *= Mathf.Deg2Rad;

        Debug.Log(transform.position + " " + plr.transform.position + " " + angle);
        rb.AddForce(new Vector2(-Mathf.Cos(angle) * speed * Time.deltaTime, -Mathf.Sin(angle) * speed * Time.deltaTime));
    }
}
