using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdExample : MonoBehaviour, IUnityAdsInitializationListener
{
    public static bool adsDisabled = false;

    [SerializeField] private string _bannerAdUnitId = "Banner_Android";

    private void Awake()
    {
        Advertisement.Initialize("5884996", true, this); // Game ID của bạn
    }

    private void Start()
    {
        if (!adsDisabled)
        {
            LoadBannerAd();
        }
    }

    public void LoadBannerAd()
    {
        if (adsDisabled || string.IsNullOrEmpty(_bannerAdUnitId)) return;

        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = () =>
            {
                Debug.Log("Banner loaded successfully.");
                ShowBannerAd();
            },
            errorCallback = (message) => Debug.Log("Banner failed to load: " + message)
        };

        Advertisement.Banner.Load(_bannerAdUnitId, loadOptions);
    }

    private void ShowBannerAd()
    {
        if (adsDisabled) return;

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(_bannerAdUnitId);
    }

    public void DisableAllAds()
    {
        adsDisabled = true;
        Debug.Log("Ads disabled by user.");

        // Ẩn banner nếu đã tải
        if (Advertisement.Banner.isLoaded)
            Advertisement.Banner.Hide();
    }

    private void OnDestroy()
    {
        if (Advertisement.Banner.isLoaded)
            Advertisement.Banner.Hide();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error} - {message}");
    }
}
