using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int countdown = 0;

    private void OnEnable()
    {
        countdown = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        countdown--;
        if (countdown <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HitSomething");
    }
}
