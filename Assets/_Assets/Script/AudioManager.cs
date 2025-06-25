using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    public AudioClip backgroundMusic;
    public AudioClip startGameMusic;
    public AudioClip buttonClickSound;
    public AudioClip buttonGoSound;
    public AudioClip vaChamDieSound;

    public Button musicButton;
    public Image musicOnIcon;
    public Image musicOffIcon;

    public Button sfxButton;
    public Image sfxOnIcon;
    public Image sfxOffIcon;

    private const string BackgroundMusicKey = "BackgroundMusic";
    private const string SoundEffectsKey = "SoundEffects";

    private bool isBackgroundMusicOn = true;
    private bool isSoundEffectsOn = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        isBackgroundMusicOn = PlayerPrefs.GetInt(BackgroundMusicKey, 1) == 1;
        isSoundEffectsOn = PlayerPrefs.GetInt(SoundEffectsKey, 1) == 1;

        ApplySettings();

        if (musicButton != null)
            musicButton.onClick.AddListener(ToggleBackgroundMusic);

        if (sfxButton != null)
            sfxButton.onClick.AddListener(ToggleSoundEffects);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySettings();
    }

    private void ApplySettings()
    {
        if (isBackgroundMusicOn)
        {
            PlayBackgroundMusic();
        }
        else
        {
            audioSource.Stop();
        }

        UpdateMusicIcon();
        UpdateSfxIcon();
    }

    public void ToggleBackgroundMusic()
    {
        isBackgroundMusicOn = !isBackgroundMusicOn;
        PlayerPrefs.SetInt(BackgroundMusicKey, isBackgroundMusicOn ? 1 : 0);
        PlayerPrefs.Save();

        if (isBackgroundMusicOn)
        {
            PlayBackgroundMusic();
        }
        else
        {
            audioSource.Stop();
        }

        UpdateMusicIcon();
    }

    public void ToggleSoundEffects()
    {
        isSoundEffectsOn = !isSoundEffectsOn;
        PlayerPrefs.SetInt(SoundEffectsKey, isSoundEffectsOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSfxIcon();
    }

    private void UpdateMusicIcon()
    {
        if (musicOnIcon != null && musicOffIcon != null)
        {
            musicOnIcon.enabled = isBackgroundMusicOn;
            musicOffIcon.enabled = !isBackgroundMusicOn;
        }
    }

    private void UpdateSfxIcon()
    {
        if (sfxOnIcon != null && sfxOffIcon != null)
        {
            sfxOnIcon.enabled = isSoundEffectsOn;
            sfxOffIcon.enabled = !isSoundEffectsOn;
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && isBackgroundMusicOn)
        {
            if (audioSource.clip != backgroundMusic)
            {
                audioSource.clip = backgroundMusic;
                audioSource.loop = true;
            }
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    // ✅ THÊM CÁC HÀM PHÁT ÂM THANH BUTTON VÀ VA CHẠM
    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null && isSoundEffectsOn)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void PlayGoSound()
    {
        if (buttonGoSound != null && isSoundEffectsOn)
        {
            audioSource.PlayOneShot(buttonGoSound);
        }
    }

    public void PlayVaChamDieSound()
    {
        if (vaChamDieSound != null && isSoundEffectsOn)
        {
            audioSource.PlayOneShot(vaChamDieSound);
        }
    }
}
