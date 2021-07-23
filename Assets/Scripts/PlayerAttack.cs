using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject _attack;
    private GameObject _attackUp;
    private GameObject _attackDown;

    private Animator _anim;

    private int _attackHash;

    [SerializeField] private KeyCode attackInput = KeyCode.J;

    [SerializeField] private int attackCD = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _attack = transform.Find("Attack").gameObject;
        _attackUp = transform.Find("Attack Up").gameObject;
        _attackDown = transform.Find("Attack Down").gameObject;

        _anim = GetComponent<Animator>();
        _attackHash = Animator.StringToHash("Attack");

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        
    }

    private void FixedUpdate()
    {
        if (attackCD > 0) attackCD--;
    }

    public void Attack()
    {
        if (Input.GetKeyDown(attackInput) && attackCD <= 0)
        {
            _attack.SetActive(true);
            _anim.SetTrigger(_attackHash);
            attackCD = 20;
        }
    }
}
