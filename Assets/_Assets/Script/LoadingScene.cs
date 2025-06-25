using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    // Th?i gian ch? tr??c khi chuy?n scene (tu? ch?nh n?u mu?n)
    public float delayTime = 2f;

    void Start()
    {
        // G?i chuy?n scene sau delayTime giây
        Invoke("LoadMainScene", delayTime);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
