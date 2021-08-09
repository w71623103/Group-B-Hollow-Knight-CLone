using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHorizontal : Attack
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnEnemyHit(Collider2D other)
    {
        base.OnEnemyHit(other);
        if(other.CompareTag("Enemy"))player.playerMovement.playerRB.AddForce(Vector2.left * 25, ForceMode2D.Impulse);
    }
}
