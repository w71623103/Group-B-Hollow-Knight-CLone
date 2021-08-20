using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Destructible : MonoBehaviour
{

    [SerializeField] protected int hp = 1;
    
    [SerializeField] protected GameObject corpse;

    [SerializeField] private GameObject money;
    [SerializeField] private int moneyDropped = 3;

    private bool isQuitting;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting && !GameManager.Instance.isLoadNewScene)
        {
            if (corpse)
            {
                var temp0 = Instantiate(corpse, transform.position, Quaternion.identity);
                temp0.transform.localScale = transform.localScale;
            }

            for (int i = 0; i < moneyDropped; i++)
            {
                var temp1 = Instantiate(money, transform.position, Quaternion.identity);
                temp1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), 10), ForceMode2D.Impulse);
            }
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
