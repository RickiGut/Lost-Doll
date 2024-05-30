using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies2 : MonoBehaviour
{
   public Transform pointA;
    public Transform pointB;
    public Transform player;
    public float speed = 2f;
    public float chaseRange = 5f;
    public float stopChaseRange = 7f;
    private Vector3 oriPos;
    private Vector3 target;
    private bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
    oriPos = transform.position;
    target = pointB.position;    
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position,player.position);
        if(isChasing)
        {
            if(distanceToPlayer < stopChaseRange)
            {
                chasePlayer();
            }else
            {
                isChasing = false;
                target = oriPos;
            }
        }
        else{
            if(distanceToPlayer < chaseRange)
            {
                isChasing = true;
            }else{
                Patrol();
            }
        }
        MoveToTarget();
    }


    void Patrol()
    {
        if(transform.position == pointA.position)
        {
            target = pointB.position;
        }else if(transform.position == pointB.position)
        {
            target = pointA.position;
        }
    }

    void chasePlayer()
    {
        target = player.position;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        if(pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position,pointB.position);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,stopChaseRange);
    }
}
