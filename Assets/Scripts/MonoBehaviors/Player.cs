using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerController playerController;
    public int soul = 0;
    public int soulMax = 99;
    public int money = 0;
    public int Hp = 5;
    public int hpMax = 5;
    
    [SerializeField] protected int stun = 0;
    [SerializeField] private int stunMax = 15;
    [SerializeField] private float knockbackForce = 10f;

    public Room room;


    public bool _isMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stun <= 0)
        {
            playerAttack.Attack();
            playerMovement.Jump();
        }
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

                soul++;
                UIManager.m_SoulChange();

                Destroy(collision.gameObject);

                break;

            case "Money":

                money++;

                UIManager.m_MoneyChange();

                Destroy(collision.gameObject);

                break;

            case "Enemy":

                Hp--;
                Knockback(collision.transform);

                UIManager.m_HealthChange();

                break;
            
            case "Spikes":

                Hp--;
                Knockback(collision.transform);

                UIManager.m_HealthChange();

                break;
            
            case "Room":

                room = collision.gameObject.GetComponent<Room>();

                break;

        }

    }
    
    public void Knockback(Transform other)
    {
        stun = stunMax;
        playerMovement.playerRB.velocity = Vector2.zero;
        playerMovement.playerRB.AddForce(new Vector2((other.position.x - transform.position.x), 0).normalized * -knockbackForce, ForceMode2D.Impulse);
        playerMovement.playerRB.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
    }
}
