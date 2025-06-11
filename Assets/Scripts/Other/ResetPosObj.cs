using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosObj : MonoBehaviour
{
     //Object Respawn 
    public Vector3 resetPosition;
    // Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.position;
    }

    public void ResetPositionObject(){
        transform.position = resetPosition;
    }
}
