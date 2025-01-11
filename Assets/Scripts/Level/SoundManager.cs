using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _secBackgroundMusic;
    [SerializeField] private AudioSource _loopingSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PlayBackgroundMusic();
        PlaySecBackgroundMusic();
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }
    public void PlayLoopingSound(AudioClip clip)
    {
        if (clip != null && _loopingSource.clip != clip)
        {
            _loopingSource.clip = clip;
            _loopingSource.loop = true;
            _loopingSource.Play();
        }
    }
    public void StopLoopingSound()
    {
        if (_loopingSource.isPlaying)
        {
            _loopingSource.Stop();
            _loopingSource.clip = null;
        }
    }

    public void PlayBackgroundMusic()
    {
        PlayLoopingSound(_backgroundMusic);
    }
    public void PlaySecBackgroundMusic()
    {
        PlayLoopingSound(_secBackgroundMusic);
    }
    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }

    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        _effectsSource.volume = volume;
    }
}
