using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerController playerController;

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
}
