using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;

    [SerializeField] private KeyCode input0 = KeyCode.J;
    [SerializeField] private KeyCode input1 = KeyCode.K;
    [SerializeField] private KeyCode input2 = KeyCode.Space;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
