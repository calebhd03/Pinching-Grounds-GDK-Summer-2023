using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleVoiceline : MonoBehaviour
{
    
    [System.Serializable] public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)] public float volume;
        [Range(.1f, 3f)] public float pitch;
        [Range(0f, 1f)] public float spatialBlend;

        public bool loop;
    }

    [Range(0f, 1f)] public float chanceOfHappening;
    public List<Sound> sounds;

    bool playingSound = false;
    AudioSource audioSource;
        
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play()
    {
        if (playingSound)
            return;

        if(Random.Range(0f, 1f)< chanceOfHappening)
        {
            int soundToPlay = Random.Range(0, sounds.Count);
            audioSource.clip = sounds[soundToPlay].clip;
            audioSource.Play();
        }
    }

    IEnumerator waitForClip()
    {
        playingSound = true;
        yield return new WaitForSeconds(audioSource.clip.length);
        playingSound = false;
    }

}
