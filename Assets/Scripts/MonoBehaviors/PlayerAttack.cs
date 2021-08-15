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
    private PlayerController _playerController;
    private PlayerAudio _playerAudio;
    private PlayerMovement _playerMovement;

    private int _attackHashH;
    private int _attackHashUP;
    private int _attackHashDown;
    [SerializeField] private int attackCD = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _attack = transform.Find("Attack").gameObject;
        _attackUp = transform.Find("Attack Up").gameObject;
        _attackDown = transform.Find("Attack Down").gameObject;

        _anim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _attackHashH = Animator.StringToHash("AttackH");
        _attackHashUP = Animator.StringToHash("AttackUP");
        _attackHashDown = Animator.StringToHash("AttackDown");

        _playerAudio = GetComponent<PlayerAudio>();
        _playerMovement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    /*void Update()
    {
        Attack();
        
    }*/

    private void FixedUpdate()
    {
        if (attackCD > 0)
        {
            attackCD--;
        }
    }

    public void Attack()
    {
        if (_playerController.input0Down && attackCD <= 0)
        {
            _playerAudio.PlayAudioAttack();
            if (_playerController.inputAxis.y > 0)
            {
                AttackUp();
            } 
            else if (_playerController.inputAxis.y < 0 && !_playerMovement.isGrounded)
            {
                AttackDown();
            }
            else
            {
                AttackNormal();
            }
        }
    }

    public void AttackNormal()
    {
        _attack.SetActive(true);
        _anim.SetTrigger(_attackHashH);
        attackCD = 20;
    }

    public void AttackUp()
    {
        _attackUp.SetActive(true);
        _anim.SetTrigger(_attackHashUP);
        attackCD = 20;
    }

    public void AttackDown()
    {
        _attackDown.SetActive(true);
        _anim.SetTrigger(_attackHashDown);
        attackCD = 20;
    }
}
