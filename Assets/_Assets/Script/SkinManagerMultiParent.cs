using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public GameObject[] skins; // Gán đúng 8 skin theo thứ tự: skin0, skin1, ..., skin7

    void Start()
    {
        int selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        Debug.Log("Skin đang được load trong Scene 2: " + selectedSkin);

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(i == selectedSkin);
        }
    }
}
