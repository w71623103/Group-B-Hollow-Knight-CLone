using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDown : Attack
{

    [SerializeField] private float bounceForceSpikes = 40f;
    [SerializeField] private float bounceForceOther = 25f;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(player);
        OnBounceHit(other);
        base.OnTriggerEnter2D(other);
    }

    protected void OnBounceHit(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            player.playerMovement.playerRB.AddForce(Vector2.up * bounceForceSpikes, ForceMode2D.Impulse);
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            player.playerMovement.playerRB.AddForce(Vector2.up * bounceForceOther, ForceMode2D.Impulse);
        }
    }
}
