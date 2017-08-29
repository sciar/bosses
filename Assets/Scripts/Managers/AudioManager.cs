using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance = null; // This + Awake sets us up so we can use singletons

    public AudioSource sfxSource;
    public float sfxVolume;
    public AudioSource musicSource;
    public float musicVolume;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    // Use this for initialization
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject); (MUST ATTACH TO ROOT MANAGER OBJECT I GUESS)
    }

    private void Start()
    {
        sfxSource.volume = GameManager.Instance.sfxVolume;
        musicSource.volume = GameManager.Instance.musicVolume;
    }

    public void SetAudioLevels()
    {
        sfxSource.volume = sfxVolume;
        musicSource.volume = musicVolume;
    }

    public void PlaySingle (AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
    
    public void RandomSFX (params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfxSource.pitch = randomPitch;
        sfxSource.clip = clips[randomIndex];
        sfxSource.Play();
    
    }

}
