using UnityEngine;
using UnityEngine.UI;

public class AudioButtonManager : MonoBehaviour
{
    [Header("Music UI")]
    [SerializeField] private Button musicButton;
    [SerializeField] private Image musicOnIcon;
    [SerializeField] private Image musicOffIcon;

    [Header("SFX UI")]
    [SerializeField] private Button sfxButton;
    [SerializeField] private Image sfxOnIcon;
    [SerializeField] private Image sfxOffIcon;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private bool isMusicMuted;
    private bool isSfxMuted;

    void Start()
    {
        // Load trạng thái đã lưu
        isMusicMuted = PlayerPrefs.GetInt("musicMuted", 0) == 1;
        isSfxMuted = PlayerPrefs.GetInt("sfxMuted", 0) == 1;

        ApplySettings();

        // Gán sự kiện khi bấm nút
        musicButton.onClick.AddListener(OnMusicButtonPressed);
        sfxButton.onClick.AddListener(OnSfxButtonPressed);
    }

    private void OnMusicButtonPressed()
    {
        isMusicMuted = !isMusicMuted;
        musicSource.mute = isMusicMuted;
        PlayerPrefs.SetInt("musicMuted", isMusicMuted ? 1 : 0);
        UpdateMusicIcon();
    }

    private void OnSfxButtonPressed()
    {
        isSfxMuted = !isSfxMuted;
        sfxSource.mute = isSfxMuted;
        PlayerPrefs.SetInt("sfxMuted", isSfxMuted ? 1 : 0);
        UpdateSfxIcon();
    }

    private void ApplySettings()
    {
        musicSource.mute = isMusicMuted;
        sfxSource.mute = isSfxMuted;
        UpdateMusicIcon();
        UpdateSfxIcon();
    }

    private void UpdateMusicIcon()
    {
        musicOnIcon.enabled = !isMusicMuted;
        musicOffIcon.enabled = isMusicMuted;
    }

    private void UpdateSfxIcon()
    {
        sfxOnIcon.enabled = !isSfxMuted;
        sfxOffIcon.enabled = isSfxMuted;
    }
}
