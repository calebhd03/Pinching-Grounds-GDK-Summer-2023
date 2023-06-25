using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTheme : MonoBehaviour
{
    [SerializeField] AudioClip introAudio;
    [SerializeField] AudioClip loopAudio;
    [SerializeField] AudioSource audioSource;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
            StartCoroutine(WaitForIntroSong());
        }
    }

    IEnumerator WaitForIntroSong()
    {
        audioSource.clip = introAudio;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = loopAudio;
        audioSource.loop = true;
        audioSource.Play();
    }
}
