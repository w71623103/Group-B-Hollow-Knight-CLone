using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int countdown = 0;

    [SerializeField] private int countMax = 10;
    
    public Player player;

    private BoxCollider2D _collider;

    private Vector2 _normalHitBoxSize;

    private void OnEnable()
    {
        countdown = countMax;
        player._isMove = false;
    }

    private void Awake()
    {
        player = GetComponentInParent<Player>();

        _collider = GetComponent<BoxCollider2D>();

        _normalHitBoxSize = _collider.size;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        countdown--;
        if (countdown <= countMax/2)
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
        Debug.Log("HitSomething");
    }

    private void OnDisable()
    {
        ResetCollider();
        player._isMove = true;
    }

    void ResetCollider()
    {
        _collider.size = _normalHitBoxSize;
    }
    
    void RemoveCollider()
    {
        _collider.size = new Vector2(0, 0);
    }
}
