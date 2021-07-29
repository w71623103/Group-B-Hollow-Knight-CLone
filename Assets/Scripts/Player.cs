using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;

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
}
