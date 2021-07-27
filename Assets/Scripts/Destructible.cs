using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    [SerializeField] private int hp = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        //release particles!!!
    }

    public void Damage(int i)
    {
        hp -= i;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
