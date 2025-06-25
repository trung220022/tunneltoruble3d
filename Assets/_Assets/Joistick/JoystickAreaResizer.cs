using UnityEngine;

public class JoystickAreaResizer : MonoBehaviour
{
    public RectTransform joystickArea;
    public Vector2 newSize = new Vector2(900f, 500f); // Kích thước mới bạn muốn

    void Start()
    {
        if (joystickArea != null)
        {
            joystickArea.sizeDelta = newSize;
        }
    }
}
