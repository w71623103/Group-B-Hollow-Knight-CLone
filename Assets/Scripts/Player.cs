using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public int soulAmount = 0;
    public int moneyAmount = 0;

    public bool _isMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAttack.Attack();
    }
    
    void FixedUpdate()
    {
        if(_isMove) playerMovement.MovementFixedUpdate();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (collision.gameObject.tag)
        {
            case "Soul":
                soulAmount++;
                Destroy(collision.gameObject);
                break;

            case "Money":
                moneyAmount++;
                Destroy(collision.gameObject);
                break;
        }
        /*
        if (collision.gameObject.tag == "Soul")
        {
            auto1 = collision;
            soulAmount++;
            Destroy(collision);
        }
        */
    }
}
