using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerController playerController;
    public PlayerAudio playerAudio;
    public Focus focus;
    public float soul = 0;
    public float soulMax = 99f;
    public int money = 0;
    public int Hp = 5;
    public int hpMax = 5;

    private int _playerHitAnim;
    
    [SerializeField] protected int stun = 0;
    [SerializeField] private int stunMax = 15;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockUpForceEnemy = 20f;
    [SerializeField] private float knockUpForceGroundSpikes = 60f;

    public Room room;


    public bool _isMove = true;

    public bool isFocusReady;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
        playerAudio = GetComponent<PlayerAudio>();
        focus = GetComponent<Focus>();
        _playerHitAnim = Animator.StringToHash("Hit");

        GameManager.m_Death += DieAnim;
        GameManager.m_Respawn += Respawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (stun <= 0)
        {
            playerAttack.Attack();
            playerMovement.Jump();
            //if(Hp < hpMax) 
            focus.focus();
        }
        
        if (soul > soulMax) soul = soulMax;
        if (soul < 0) soul = 0;
    }
    
    void FixedUpdate()
    {
        
        if (stun > 0)
        {
            stun--;
        }
        else
        {
            if(_isMove) playerMovement.Movement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {

        switch (collision.gameObject.tag)

        {

            case "Soul":
                Destroy(collision.gameObject);
                soul += 11f;
                checkFocusReady();
                UIManager.m_SoulChange();

                

                break;

            case "Money":
                Destroy(collision.gameObject);
                money++;

                UIManager.m_MoneyChange();

                

                break;

            case "Enemy":

                Hit(1, collision.transform, knockbackForce,knockUpForceEnemy);
                

                break;
            
            case "Spikes":

                
                Hit(1, collision.transform, knockbackForce, knockUpForceGroundSpikes);

                break;
            
            case "Spike":

                Hit(1, collision.transform, knockbackForce, knockUpForceEnemy);

                break;
            
            case "Room":

                room = collision.gameObject.GetComponent<Room>();

                break;
            
            case "End":

                GameManager.m_GameEnd();

                break;

        }

    }
    
    public void Knockback(Transform other, float knockBackForce, float knockUpForce)
    {
        stun = stunMax;
        playerMovement.playerRB.velocity = Vector2.zero;
        playerMovement.playerRB.AddForce(new Vector2((other.position.x - transform.position.x), 0).normalized * -knockBackForce, ForceMode2D.Impulse);
        playerMovement.playerRB.AddForce(Vector2.up * knockUpForce, ForceMode2D.Impulse);
        playerMovement.playerAN.SetTrigger(_playerHitAnim);
        playerAudio.PlayAudioHurt();
    }


    public void Hit(int damage, Transform other, float knockBackForce, float knockUpForce)
    {
        if (stun <= 0)
        {
            Hp -= damage;
            Knockback(other, knockBackForce, knockUpForce);
            UIManager.m_HealthChange();
            if (Hp <= 0)
            {
                stun = 60;
                GameManager.m_Death();
            }
        }
    }

    public void DieAnim()
    {
        playerMovement.playerAN.SetTrigger("Die");
    }

    public void decreaseSoul(float decreaseVal) 
    {
        soul -= decreaseVal;
        checkFocusReady();
        UIManager.m_SoulChange();
    }

    public void heal(int val)
    {
        if(Hp+val <= hpMax) Hp+=val;
        UIManager.m_HealthChange();
    }

    public void healMax()
    {
        Hp = hpMax;
    }

    public void checkFocusReady()
    {
        if (!isFocusReady && soul >= 33)
        {
            isFocusReady = true;
            playerAudio.PlayFocusReady();
        }

        if (isFocusReady && soul < 33)
        {
            isFocusReady = false;
        }
    }

    public void Respawn()
    {
        Hp = hpMax;
        soul = 0;
        UIManager.m_SoulChange();
        UIManager.m_HealthChange();
        GetComponent<SpriteRenderer>().color = Color.white;
        stun = 0;
        playerMovement.playerAN.SetTrigger("Respawn");
    }
}
