using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "ScriptableObjects/PlayerInput", order = 1)]
public class PlayerInputSO : ScriptableObject
{
    public KeyCode key0 = KeyCode.J;
    public KeyCode key1 = KeyCode.Space;
    public KeyCode keyJump = KeyCode.K;
    public string axisH = "Horizontal";
    public string axisV = "Vertical";
}
