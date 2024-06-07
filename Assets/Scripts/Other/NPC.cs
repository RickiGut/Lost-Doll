using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public SpriteRenderer player;
     private SpriteRenderer npc;

    // Start is called before the first frame update
    void Start()
    {   
        npc = GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < player.transform.position.x){
            npc.flipX = false;
        }else if(transform.position.x > player.transform.position.x){
            npc.flipX = true;
        }
    }
}
