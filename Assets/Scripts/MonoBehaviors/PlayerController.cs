using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerInputSO playerInput;

    public Vector2 inputAxis;
    public bool inputJumpDown;
    public bool inputJump;
    public bool input0Down;
    public bool input1Down;
    public bool inputFocus;

    void Update()
    {
        inputAxis = new Vector2(Input.GetAxisRaw(playerInput.axisH), Input.GetAxisRaw(playerInput.axisV));
        inputJumpDown = Input.GetKeyDown(playerInput.keyJump);
        inputJump = Input.GetKey(playerInput.keyJump);
        input0Down = Input.GetKeyDown(playerInput.key0);
        input1Down = Input.GetKeyDown(playerInput.key1);
        inputFocus = Input.GetKey(playerInput.keyFocus);
    }
}
