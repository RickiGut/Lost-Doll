using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicator2Hide : MonoBehaviour
{
    [SerializeField]
    private GameObject indicatorBg;
    [SerializeField]
    private GameObject indicatorTeks;

    // Start is called before the first frame update
    void Start()
    {
        indicatorBg.SetActive(false);
        indicatorTeks.SetActive(false);
            indicatorBg.SetActive(true);
            indicatorTeks.SetActive(true);
    }

    // Update is called once per frame
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.CompareTag("Player")){
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.CompareTag("Player")){
    //         indicatorBg.SetActive(false);
    //         indicatorTeks.SetActive(false);
    //     }
    // }
}
