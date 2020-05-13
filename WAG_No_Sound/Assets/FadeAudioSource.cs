using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeAudioSource
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, float startTransition)
    {
        float currentTime = 0;
        float currentTimeSinceStart = 0;
        float start = audioSource.volume;
        while (currentTimeSinceStart < startTransition)
        {
            Debug.Log(currentTimeSinceStart);
            currentTimeSinceStart += Time.deltaTime;
            yield return null;
        }
        while (currentTime < duration)
        {
            
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
