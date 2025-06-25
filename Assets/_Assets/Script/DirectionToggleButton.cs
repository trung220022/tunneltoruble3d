using UnityEngine;
using UnityEngine.UI;

public class DirectionToggleButton : MonoBehaviour
{
    public GameObject rightImage;
    public GameObject leftImage;
    public RectTransform joystickRect; // Kéo RectTransform của joystick vào đây

    private bool isRight = true;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ToggleDirection);

        // Đọc trạng thái từ PlayerPrefs
        isRight = PlayerPrefs.GetInt("JoystickDirection", 1) == 1;
        UpdateImages();
        UpdateJoystickPosition();
    }

    private void ToggleDirection()
    {
        isRight = !isRight;
        UpdateImages();
        UpdateJoystickPosition();

        // Lưu lại
        PlayerPrefs.SetInt("JoystickDirection", isRight ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateImages()
    {
        if (rightImage != null) rightImage.SetActive(isRight);
        if (leftImage != null) leftImage.SetActive(!isRight);
    }

    private void UpdateJoystickPosition()
    {
        if (joystickRect == null) return;

        if (isRight)
        {
            joystickRect.anchorMin = new Vector2(1f, 0f);  // góc dưới phải
            joystickRect.anchorMax = new Vector2(1f, 0f);
            joystickRect.anchoredPosition = new Vector2(-500f, 200f); // Cách phải -500 lên 200
        }
        else
        {
            joystickRect.anchorMin = new Vector2(0f, 0f);  // góc dưới trái
            joystickRect.anchorMax = new Vector2(0f, 0f);
            joystickRect.anchoredPosition = new Vector2(500f, 200f);  // Cách trái 500, lên 200
        }
    }
}
