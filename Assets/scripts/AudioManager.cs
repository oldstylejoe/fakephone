/* Joe Snider
 * 4/18
 * 
 * Only 2 sounds, so just control it all here.
 * 
 * Added in playing the messages from here. JS 11/2018
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    [Header("The sounds. Clips set in source.")]
    public AudioSource phoneSource;
    public AudioSource textSource;
    public AudioSource messageSource;

    [Header("Random pitch jitter.")]
    public float lowPitchRange = .99f;
    public float highPitchRange = 1.01f;

    [Header("The audio messages to cycle through.")]
    public List<AudioClip> audioMessages;
    private int nextMessage = 0;

    public void RingPhone()
    {
        phoneSource.pitch = Random.Range(lowPitchRange, highPitchRange);
        phoneSource.loop = true;
        phoneSource.Play();
    }
    public void StopPhone()
    {
        phoneSource.Stop();
    }

    public void AlertText()
    {
        textSource.pitch = Random.Range(lowPitchRange, highPitchRange);
        textSource.Play();
    }

    /// <summary>
    /// just cycles through.
    /// </summary>
    public void PlayNextAudioMessage()
    {
        messageSource.clip = audioMessages[nextMessage];
        messageSource.Play();
        nextMessage = (nextMessage + 1) % audioMessages.Count;
    }
    
}

