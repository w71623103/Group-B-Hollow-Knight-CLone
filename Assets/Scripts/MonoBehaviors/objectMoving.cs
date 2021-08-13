using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMoving : MonoBehaviour
{

    //vector.normalize returns an e-> to your direction you want
    // Start is called before the first frame update
    public GameObject target;
    public float mSpeed = 2f;
    public Rigidbody2D objRB;
    //public Vector3 auto1;
    //public Vector2 auto2;

    void Start()
    {
        objRB = GetComponent<Rigidbody2D>();
        
        target = GameObject.FindWithTag("Player");
        //auto1 = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 temp = target.transform.position - transform.position;
        //auto1 = temp;
        Vector2 vel = new Vector2(temp.x, temp.y);
        vel.Normalize();
        //auto2 = vel;
        objRB.velocity = vel * mSpeed;
    }
}
