using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Destructible : MonoBehaviour
{

    [SerializeField] protected int hp = 1;
    
    [SerializeField] private GameObject corpse;

    [SerializeField] private GameObject money;
    [SerializeField] private int moneyDropped = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected void OnDestroy()
    {
        Instantiate(corpse, transform.position, Quaternion.identity);

        for (int i = 0; i < moneyDropped; i++)
        {
            var temp = Instantiate(money, transform.position, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), 10), ForceMode2D.Impulse);
        }
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
