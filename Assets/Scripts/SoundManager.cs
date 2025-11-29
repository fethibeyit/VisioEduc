using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip clickSound;

    private AudioSource audioSource;
    private AudioSource musicSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySFX(string soundName)
    {
        var clip = Resources.Load<AudioClip>("Sounds/" + soundName);
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void PlayMusic(string musicName, bool loop = true)
    {
        var clip = Resources.Load<AudioClip>("Sounds/" + musicName);
        if (clip != null)
            musicSource.clip = clip;
            musicSource.loop = loop;
        {
            musicSource.Play();
        }
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayCorrect()
    {
        audioSource.PlayOneShot(correctSound);
    }

    public void PlayWrong()
    {
        audioSource.PlayOneShot(wrongSound);
    }
}
