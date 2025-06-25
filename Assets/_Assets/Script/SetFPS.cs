using UnityEngine;

public class SetFPS : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0;          // Vô hiệu hóa VSync để không giới hạn FPS theo màn hình
        Application.targetFrameRate = 60;        // Đặt giới hạn khung hình tối đa là 60 FPS
    }
}
