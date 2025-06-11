using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerParallax : MonoBehaviour
{
    public float parralaxFact;

    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta *  parralaxFact;
        transform.localPosition = newPos;
    }

}
