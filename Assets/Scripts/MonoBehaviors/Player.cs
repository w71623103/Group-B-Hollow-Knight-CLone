using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerController playerController;
    
    public int soulAmount = 0;
    public int moneyAmount = 0;

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
        playerAttack.Attack();
        playerMovement.Jump();
    }
    
    void FixedUpdate()
    {
        if(_isMove) playerMovement.Movement();
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
    }
}
