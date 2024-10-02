using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public override void Initialize()
    {
        base.Initialize();
    }
    float attachDamage = 0.1f;
    protected override void Move()
    {
        // Move towards the player
        Vector3 direction = (GameManager.Player.transform.position - transform.position).normalized;
        transform.position += direction * Time.deltaTime * moveSpeed;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(attachDamage);
        }
    }
}
