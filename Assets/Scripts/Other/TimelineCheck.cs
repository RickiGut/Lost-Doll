using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineCheck : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject canvasToHide;
    public GameObject canvas2ToHide;

    void Start(){
        canvasToHide.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            timeline.Play();
            if(canvasToHide != null){
                canvasToHide.SetActive(true);
                canvas2ToHide.SetActive(false);
            }
        }
    }

    void OnEnable(){
        if(timeline != null){
            timeline.stopped += OnTimelineStopped;
        }
    }

    void OnDisable(){
        if(timeline != null){
            timeline.stopped -= OnTimelineStopped;
        }
    }

    void OnTimelineStopped(PlayableDirector director){
        if(canvasToHide != null){
            canvasToHide.SetActive(true);
            canvas2ToHide.SetActive(true);
            Destroy(this.gameObject);
            Destroy(timeline.gameObject);
        }
    }

}
