using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int countdown = 0;

    [SerializeField] private int countHit = 5;
    [SerializeField] private int countRecover = 10;
    
    public Player player;

    private BoxCollider2D _collider;

    private Vector2 _normalHitBoxSize;
    private Vector3 _normalAttackBoxSize;

    private void OnEnable()
    {
        countdown = countHit + countRecover;
        player._isMove = false;
    }

    private void Awake()
    {
        player = GetComponentInParent<Player>();

        _collider = GetComponent<BoxCollider2D>();

        _normalHitBoxSize = _collider.size;
        _normalAttackBoxSize = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        countdown--;
        if (countdown <= countRecover)
        {
            RemoveCollider();
        }
        if (countdown <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        OnDestructibleHit(other);
        OnEnemyHit(other);
    }

    private void OnDisable()
    {
        ResetCollider();
        player._isMove = true;
    }

    void ResetCollider()
    {
        _collider.size = _normalHitBoxSize;
        transform.localScale = _normalAttackBoxSize;
    }
    
    void RemoveCollider()
    {
        _collider.size = new Vector2(0, 0);
        transform.localScale = Vector3.zero;
    }

    protected virtual void OnDestructibleHit(Collider2D other)
    {
        if (other.CompareTag("Destructible"))
        {
            other.GetComponent<Destructible>().Damage(1);
        }
    }
    
    protected virtual void OnEnemyHit(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Damage(1);
            other.GetComponent<Enemy>().Knockback(transform.GetComponentInParent<Transform>());
        }
    }
}
