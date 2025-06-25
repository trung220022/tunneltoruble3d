using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSelector : MonoBehaviour
{  

    public void OnSkinButtonClicked(int skinIndex)
    {
        Debug.Log("Đã chọn skin: " + skinIndex);

        PlayerPrefs.SetInt("SelectedSkin", skinIndex);
        PlayerPrefs.Save();
        Debug.Log("Skin vừa được ghi vào PlayerPrefs: " + skinIndex);
    }
}
