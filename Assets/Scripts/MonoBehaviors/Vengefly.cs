using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vengefly : Enemy
{
    [SerializeField] private float playerActivateDistance = 8f;

    [SerializeField] private float spd = 2f;

    private float dir = 1; // -1 is left, 1 is right

    private bool isChasing;

    private Vector2 targetPos;

    private Vector2 currPos;

    private float lerp = 1;

    private int isChasingHash;
    private int turnHash;
    private int startleHash;

    protected override void Start()
    {
        base.Start();
        isChasingHash = Animator.StringToHash("isChasing");
        turnHash = Animator.StringToHash("Turn");
        startleHash = Animator.StringToHash("Startle");
    }
    
    protected override void Behavior()
    {
        CheckDistanceFromPlayer();
        if(!_audioSource.isPlaying)_audioSource.Play();
        if (isChasing)
        {
            if (lerp >= 1)
            {
                SetNewTargetPos();
            }
            transform.position = new Vector2(Mathf.Lerp(currPos.x, targetPos.x, lerp), Mathf.Lerp(currPos.y, targetPos.y, lerp));
            lerp += spd * Time.deltaTime;
        }
    }

    private void CheckDistanceFromPlayer()
    {
        if (!isChasing && DistanceFromPlayer() < playerActivateDistance)
        {
            isChasing = true;
            _anim.SetBool(isChasingHash, true);
            _anim.SetTrigger(startleHash);
        }
    }

    private void SetNewTargetPos()
    {
        Vector3 temp = player.transform.position - transform.position;
        //auto1 = temp;
        Vector2 vel = new Vector2(temp.x, temp.y);
        if (Mathf.Abs(vel.x) > Mathf.Abs(vel.y))
        {
            vel = new Vector2(Mathf.Abs(vel.y) * Mathf.Sign(vel.x), vel.y);
        }
        vel.Normalize();
        vel = vel * spd;
        //auto2 = vel;
        
        if (Mathf.Sign(vel.x) != Mathf.Sign(dir))
        {
            Turn();
        }
        
        var position = transform.position;
        targetPos = new Vector2(position.x + vel.x, position.y + vel.y);
        currPos = position;
        lerp = 0;
    }

    private void ReflectTargetPos(float xNormal, float yNormal)
    {
        Vector2 temp = (Vector3)targetPos - transform.position;

        if (xNormal != 0)
        {
            temp = new Vector2(Mathf.Sign(xNormal) * Mathf.Abs(temp.x), temp.y);
        }
        if (yNormal != 0)
        {
            temp = new Vector2(temp.x, Mathf.Sign(yNormal) * Mathf.Abs(temp.y));
        }
        
        if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y))
        {
            temp = new Vector2(Mathf.Abs(temp.y) * Mathf.Sign(temp.x), temp.y);
        }
        temp.Normalize();
        temp = temp * spd;

        if (Mathf.Sign(temp.x) != Mathf.Sign(dir))
        {
            Turn();
        }
        
        var position = transform.position;
        targetPos = new Vector2(position.x + temp.x, position.y + temp.y);
        currPos = position;
        lerp = 0;
        Debug.Log(targetPos);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Vector3 hit = other.contacts[0].normal;
            Debug.Log(hit);
            float angle = Vector3.Angle(hit, Vector3.up);
 
            if (Mathf.Approximately(angle, 0))
            {
                //Down
                Debug.Log("Down");
                ReflectTargetPos(0, 1);
            }
            if(Mathf.Approximately(angle, 180))
            {
                //Up
                Debug.Log("Up");
                ReflectTargetPos(0, -1);
            }
            if(Mathf.Approximately(angle, 90)){
                // Sides
                Vector3 cross = Vector3.Cross(Vector3.forward, hit);
                if (cross.y > 0)
                { // left side of the player
                    Debug.Log("Left");
                    ReflectTargetPos(-1, 0);
                }
                else
                { // right side of the player
                    Debug.Log("Right");
                    ReflectTargetPos(1, 0);
                }
            }
        }
    }

    public override void Knockback(Transform other)
    {
        lerp = 1;
        base.Knockback(other);
    }

    private void Turn()
    {
        var localScale = transform.localScale;
        localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        transform.localScale = localScale;
        dir = -dir;
        _anim.SetTrigger(turnHash);
    }
}
