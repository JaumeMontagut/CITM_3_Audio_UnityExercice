using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreEntryTitle : MonoBehaviour
{
    // Start is called before the first frame update
   public AudioSource preAudio;
   public AudioSource loopAudio;
    void Start()
    {
        preAudio.Play();
        loopAudio.PlayDelayed(preAudio.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        //if(!audio.isPlaying)
        //{
        //    audio.clip = loopClip;
        //    audio.loop = true;
        //    audio.Play();
        //}
    }
}
