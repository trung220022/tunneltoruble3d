using UnityEngine;

public class JoystickPositionLoader : MonoBehaviour
{
    public RectTransform joystickRect;

    private void Start()
    {
        // Delay nhẹ để UI canvas layout xong
        Invoke(nameof(ApplyJoystickPosition), 0.01f);
    }

    private void ApplyJoystickPosition()
    {
        bool isRight = PlayerPrefs.GetInt("JoystickDirection", 1) == 1;
        SetJoystickPosition(isRight);
    }

    void SetJoystickPosition(bool isRight)
    {
        if (joystickRect == null) return;

        if (isRight)
        {
            joystickRect.anchorMin = new Vector2(1f, 0f);
            joystickRect.anchorMax = new Vector2(1f, 0f);
            
            joystickRect.anchoredPosition = new Vector2(-500f, 250f);
        }
        else
        {
            joystickRect.anchorMin = new Vector2(0f, 0f);
            joystickRect.anchorMax = new Vector2(0f, 0f);
            
            joystickRect.anchoredPosition = new Vector2(500f, 250f);
        }
    }
}
