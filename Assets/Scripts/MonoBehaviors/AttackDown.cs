using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDown : Attack
{

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(player);
        player.playerMovement.playerRB.AddForce(Vector2.up * 40, ForceMode2D.Impulse);
        base.OnTriggerEnter2D(other);
    }
}
