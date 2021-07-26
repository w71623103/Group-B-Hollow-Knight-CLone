using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHorizontal : Attack
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(player);
        player.playerMovement.playerRB.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        base.OnTriggerEnter2D(other);
    }
}
