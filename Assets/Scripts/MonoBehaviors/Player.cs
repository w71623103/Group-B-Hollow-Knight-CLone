using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerController playerController;
    public int soul = 0;
    public int money = 0;
    public int Hp = 5;

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
        playerAttack.Attack();
        playerMovement.Jump();
    }
    
    void FixedUpdate()
    {
        if(_isMove) playerMovement.Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {

        switch (collision.gameObject.tag)

        {

            case "Soul":

                soul++;

                Destroy(collision.gameObject);

                break;

            case "Money":

                money++;

                Destroy(collision.gameObject);

                break;

            case "Enemy":

                Hp--;

                break;
            
            case "Room":

                room = collision.gameObject.GetComponent<Room>();

                break;

        }

    }
}
