using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip[] playlist;
    private AudioSource bgMusic;
    private int currentIndex = 0;
    private bool isManuallyPaused = false;
    private float volumeBeforeMute = 1f;
    private float nextCheckTime = 0f;
    private float checkCooldown = 0.5f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            bgMusic = GetComponent<AudioSource>();
            bgMusic.ignoreListenerPause = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayCurrent();
    }

    private void Update()
    {
        if (Time.unscaledTime < nextCheckTime) return;

        if (!bgMusic.isPlaying && !isManuallyPaused)
        {
            PlayNext();
            nextCheckTime = Time.unscaledTime + checkCooldown;
        }
    }

    private void PlayCurrent()
    {
        if (playlist.Length == 0) return;

        bgMusic.clip = playlist[currentIndex];
        bgMusic.Play();
        Debug.Log($"Playing: {bgMusic.clip.name}");
    }

    private void PlayNext()
    {
        currentIndex = (currentIndex + 1) % playlist.Length;
        PlayCurrent();
    }

    public void PauseMusic()
    {
        if (bgMusic.isPlaying)
        {
            isManuallyPaused = true;
            bgMusic.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (isManuallyPaused)
        {
            isManuallyPaused = false;
            bgMusic.UnPause();
        }
    }

    public void SetVolume(float volume)
    {
        bgMusic.volume = volume;
        if (volume > 0f) volumeBeforeMute = volume;
    }

    public float GetVolume()
    {
        return bgMusic.volume;
    }

    public void ToggleMute()
    {
        if (bgMusic.volume > 0f)
            SetVolume(0f);
        else
            SetVolume(volumeBeforeMute);
    }

    public bool IsMuted()
    {
        return bgMusic.volume == 0f;
    }
}
