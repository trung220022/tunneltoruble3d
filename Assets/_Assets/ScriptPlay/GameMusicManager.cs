using UnityEngine;
using System.Collections;

public class GameMusicManager : MonoBehaviour
{
    public AudioClip readyGoClip;
    public AudioClip backgroundMusicClip;
    public AudioClip buttonClickClip;

    [Range(0f, 1f)]
    public float readyGoVolume = 1f;
    [Range(0f, 1f)]
    public float backgroundVolume = 0.5f;
    [Range(0f, 1f)]
    public float buttonClickVolume = 1f;

    private AudioSource audioSource;

    private bool isMusicOn = true;
    private bool isSoundOn = true;

    private const string BackgroundMusicKey = "BackgroundMusic";
    private const string SoundEffectsKey = "SoundEffects";

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        isMusicOn = PlayerPrefs.GetInt(BackgroundMusicKey, 1) == 1;
        isSoundOn = PlayerPrefs.GetInt(SoundEffectsKey, 1) == 1;

        if (isMusicOn)
        {
            StartCoroutine(PlayReadyGoThenBackground());
        }
        else
        {
            audioSource.Stop();
        }
    }

    IEnumerator PlayReadyGoThenBackground()
    {
        if (readyGoClip != null)
        {
            audioSource.clip = readyGoClip;
            audioSource.volume = readyGoVolume;
            audioSource.loop = false;
            audioSource.Play();

            yield return new WaitForSeconds(readyGoClip.length);
        }

        if (backgroundMusicClip != null)
        {
            audioSource.clip = backgroundMusicClip;
            audioSource.volume = backgroundVolume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SetBackgroundVolume(float volume)
    {
        backgroundVolume = volume;
        if (audioSource.clip == backgroundMusicClip)
        {
            audioSource.volume = backgroundVolume;
        }
    }

    public void SetReadyGoVolume(float volume)
    {
        readyGoVolume = volume;
        if (audioSource.clip == readyGoClip)
        {
            audioSource.volume = readyGoVolume;
        }
    }

    // ✅ Toggle âm nhạc nền
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        PlayerPrefs.SetInt(BackgroundMusicKey, isMusicOn ? 1 : 0);
        PlayerPrefs.Save();

        if (isMusicOn)
        {
            StartCoroutine(PlayReadyGoThenBackground());
        }
        else
        {
            audioSource.Stop();
        }
    }

    // ✅ Toggle hiệu ứng âm thanh (SFX)
    public void ToggleSoundEffects()
    {
        isSoundOn = !isSoundOn;

        PlayerPrefs.SetInt(SoundEffectsKey, isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ✅ Phát âm thanh khi nhấn button (chỉ nếu SFX bật)
    public void PlayButtonClickSound()
    {
        if (buttonClickClip != null && isSoundOn)
        {
            audioSource.PlayOneShot(buttonClickClip, buttonClickVolume);
        }
    }
}
