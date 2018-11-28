/* Joe Snider
 * 4/18
 * 
 * Only 2 sounds, so just control it all here.
 * */

using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    [Header("The sounds. Clips set in source.")]
    public AudioSource phoneSource;
    public AudioSource textSource;

    [Header("Random pitch jitter.")]
    public float lowPitchRange = .99f;
    public float highPitchRange = 1.01f;

    public void RingPhone()
    {
        phoneSource.pitch = Random.Range(lowPitchRange, highPitchRange);
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
    
}

