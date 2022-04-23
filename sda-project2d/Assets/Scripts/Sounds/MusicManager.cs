using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Music
{
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
}

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private Music music;

    private AudioSource musicSource;
    public AudioSource MusicSource { get { return musicSource; } }

    public Music Music { get { return music; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }
    
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    
}
