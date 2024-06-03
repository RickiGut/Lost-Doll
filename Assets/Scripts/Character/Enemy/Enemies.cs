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

 //Flip X
 SpriteRenderer spriteRenderEnemies;

//Animator
Animator animator;

void Start()
{
    oriPositionEnemies = transform.position;
    targetEnemies = pointB.position;
    spriteRenderEnemies = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
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
        animator.SetBool("kuntiNgejar",false);
     }
    }
    else{
        if(distanceToPlayer < chaseRange)
        {
            isChasing = true;
            animator.SetBool("kuntiNgejar",true);
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
        spriteRenderEnemies.flipX = false;
    }else if(Vector3.Distance(transform.position,pointB.position) < 0.1f)
    {
        targetEnemies = pointA.position;
        spriteRenderEnemies.flipX = true;
    }
}

void ChasePlayer()
{
    targetEnemies = player.position;
    if(player.position.x > transform.position.x)
    {
        spriteRenderEnemies.flipX = false;
    }else{
        spriteRenderEnemies.flipX = true;
    }
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
