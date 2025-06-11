using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgParallax : MonoBehaviour
{
    public CameraParallax parallaxCamera;
    List<LayerParallax> LayerParallaxs = new List<LayerParallax>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<CameraParallax>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        SetLayers();
    }

    void SetLayers()
    {
        LayerParallaxs.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            LayerParallax layer = transform.GetChild(i).GetComponent<LayerParallax>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                LayerParallaxs.Add(layer);
            }
        }
    }

    void Move(float delta)
    {
        foreach (LayerParallax layer in LayerParallaxs)
        {
            layer.Move(delta);
        }
    }
}
