using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructible
{
    [SerializeField] protected int stun = 0;
    [SerializeField] private int stunMax = 20;
    [SerializeField] private float knockbackForce = 10f;

    [SerializeField] private float awakeRange = 49f;

    protected Rigidbody2D _rb;
    protected AudioSource _audioSource;
    protected Animator _anim;

    protected GameObject player;

    private bool isAwake = false;
    
    // Start is called before the first frame update
    protected void Start()
    {

        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void FixedUpdate()
    {
        if (!isAwake)
        {
            if (DistanceFromPlayer() < awakeRange) isAwake = true;
            if(_audioSource.isPlaying) _audioSource.Stop();
        }
        else
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
    }

    public virtual void Knockback(Transform other)
    {
        stun = stunMax;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2((other.position.x - transform.position.x), 0).normalized * -knockbackForce, ForceMode2D.Impulse);
        Debug.Log(_rb.velocity);
    }

    protected virtual void Behavior()
    {
        
    }
    
    protected float DistanceFromPlayer()
    {
        return Mathf.Abs((transform.position - player.transform.position).magnitude);
    }
}
