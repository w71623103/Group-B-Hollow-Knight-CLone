using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpike : MonoBehaviour
{

    private FallingSpike fallingSpike;

    private void Start()
    {
        fallingSpike = GetComponentInChildren<FallingSpike>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            fallingSpike.Fall();
        }
    }
}
