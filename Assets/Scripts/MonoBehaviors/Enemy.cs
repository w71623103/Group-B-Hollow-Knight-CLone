using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructible
{
    [SerializeField] protected int stun = 0;
    [SerializeField] private int stunMax = 20;
    [SerializeField] private float knockbackForce = 10f;
    
    protected Rigidbody2D _rb;
    
    // Start is called before the first frame update
    protected void Start()
    {

        _rb = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        if (stun > 0)
        {
            stun--;
        }
        else
        {
            Behavior();
        }
    }

    public void Knockback(Transform other)
    {
        stun = stunMax;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2((other.position.x - transform.position.x), 0).normalized * knockbackForce, ForceMode2D.Impulse);
        Debug.Log(_rb.velocity);
    }

    protected virtual void Behavior()
    {
        
    }
}
