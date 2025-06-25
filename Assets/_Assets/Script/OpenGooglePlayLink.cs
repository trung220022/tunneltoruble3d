using UnityEngine;

public class OpenGooglePlayLink : MonoBehaviour
{
    // Đặt link tới trang Google Play của bạn ở đây
    public string googlePlayURL = "https://play.google.com/store/apps/details?id=com.caketower.building.jump.capybara&hl=vi";

    public void OpenLink()
    {
        Application.OpenURL(googlePlayURL);
    }
}
