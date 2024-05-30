using UnityEngine;

public class Enemies : MonoBehaviour
{
 public Transform pointA;
 public Transform pointB;
 public Transform player;
 public float speed = 3.5f;
 public float chaseRange = 5f;
 public float stopChase = 5f;

 private Vector3 oriPositionEnemies;
 private Vector3 targetEnemies;
 private bool isChasing = false;

void Start()
{
    oriPositionEnemies = transform.position;
    targetEnemies = pointB.position;
}

void Update()
{
    float distanceToPlayer = Vector3.Distance(transform.position,player.position);
    if(isChasing)
    {
     if(distanceToPlayer < stopChase)
     {
        ChasePlayer();
     }else{
        isChasing = false;
        targetEnemies = GetNearestPatrolPoint();
     }
    }
    else{
        if(distanceToPlayer < chaseRange)
        {
            isChasing = true;
        }
        else{
            Patrol();
        }
    }
    MoveToTarget();
}


void Patrol()
{
    if(Vector3.Distance(transform.position,pointA.position) < 0.1f)
    {
        targetEnemies = pointB.position;
    }else if(Vector3.Distance(transform.position,pointB.position) < 0.1f)
    {
        targetEnemies = pointA.position;
    }
}

void ChasePlayer()
{
    targetEnemies = player.position;
}

void MoveToTarget()
{
    transform.position = Vector3.MoveTowards(transform.position,targetEnemies,speed * Time.deltaTime);
}

Vector3 GetNearestPatrolPoint()
{
    float distanceToPointA = Vector3.Distance(transform.position,pointA.position);
    float distanceToPointB = Vector3.Distance(transform.position,pointB.position);

    return distanceToPointA < distanceToPointB ? pointA.position : pointB.position;
}

    void OnDrawGizmos()
    {
        if(pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position,pointB.position);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,chaseRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,stopChase);
    }


}
