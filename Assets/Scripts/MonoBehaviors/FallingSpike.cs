using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : Destructible
{
    private bool _spawnedCorpse = false;
    protected new void OnDestroy()
    {
        Debug.Log("Particles?");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!_spawnedCorpse)
            {
                Instantiate(corpse, transform.position, Quaternion.identity);
                _spawnedCorpse = true;
            }
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Damage(1);
            other.GetComponent<Enemy>().Knockback(transform.GetComponentInParent<Transform>());
        }
    }
    
    
}
