using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightningEffect : MonoBehaviour
{
    public ParticleSystem lightningParticle; // Assign di Inspector
    public Light2D lightningLight; // Assign Light 2D di Inspector
    public Volume blackAndWhiteVolume; // Assign Box Volume di Inspector
    private bool isTriggered = false;

    [SerializeField] float timeLightning;
    [SerializeField] float timeLightningWait;
    [SerializeField] float timeAfterLightning;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            StartCoroutine(TriggerLightningEffect());
        }
    }

    private IEnumerator TriggerLightningEffect()
    {
        isTriggered = true;

        // Aktifkan Particle System petir
        lightningParticle.Play();

        // Aktifkan Light 2D selama 0.1 detik
        lightningLight.enabled = true;
        yield return new WaitForSeconds(timeLightning);
        lightningLight.enabled = false;

        // Tunggu 0.5 detik lagi sebelum efek B&W aktif (total delay 1 detik dari awal petir)
        yield return new WaitForSeconds(timeLightningWait);

        // Aktifkan efek Black & White
        blackAndWhiteVolume.weight = 1f;

        yield return new WaitForSeconds(timeAfterLightning); // Efek bertahan selama 2 detik

        // Kembalikan efek ke normal
        blackAndWhiteVolume.weight = 0f;
        isTriggered = false;
    }
}
