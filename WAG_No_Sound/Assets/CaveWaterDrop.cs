using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWaterDrop : MonoBehaviour
{
    public List<AudioClip> waterDropSounds;
    AudioSource audioSource;

    float currentTime;
    float liveTime = 1.0f;
    bool isPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayed)
        {
            currentTime = Time.time;
            isPlayed = true;
            liveTime = Random.Range(0.5f, 2.0f);
        }
        else if(Time.time - currentTime > liveTime)
        {
            int random = Random.Range(0, waterDropSounds.Count);
            audioSource.PlayOneShot(waterDropSounds[random]);
            isPlayed = false;
        }
    }
}
