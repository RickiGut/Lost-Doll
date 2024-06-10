using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject FIndicator;
    public GameObject BgIndicator;

    // Start is called before the first frame update
    void Start()
    {
        FIndicator.SetActive(false);
        BgIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                FIndicator.SetActive(true);
                BgIndicator.SetActive(true);
                print("Hello");
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                FIndicator.SetActive(false);
                BgIndicator.SetActive(false);
            }
        }

     public void HideIndicator(){
        FIndicator.SetActive(false);
        BgIndicator.SetActive(false);
    }
}
