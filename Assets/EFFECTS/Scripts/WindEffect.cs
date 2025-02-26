using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [SerializeField] GameObject WindBox;
    void Start()
    {
        WindBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        WindBox.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        WindBox.SetActive(false);
    }


}
