using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource; // Référence vers l'AudioSource
    public float fadeDuration = 5f; // Durée des fades (en secondes)

    public void RestartMusicWithFade()
    {
        StartCoroutine(RestartWithOverlappingFade(audioSource, fadeDuration));
    }

    private IEnumerator RestartWithOverlappingFade(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float fadeProgress = 0f;

        // On commence immédiatement à redémarrer la musique au début
        audioSource.time = 0f; // Redémarre à la position 0
        audioSource.Play();

        while (fadeProgress < duration)
        {
            fadeProgress += Time.deltaTime;

            // Augmente progressivement le volume de la nouvelle lecture
            audioSource.volume = Mathf.Lerp(0f, startVolume, fadeProgress / duration);

            yield return null;
        }

        // Assure que le volume est correctement restauré
        audioSource.volume = startVolume;
    }
}

