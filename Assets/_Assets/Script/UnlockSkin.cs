using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnlockSkin : MonoBehaviour
{
    public Button unlockSkin; // Nút mở khóa
    public Button[] skinToUnlock; // Danh sách skin có thể unlock
    private List<int> availableIndices = new List<int>(); // Danh sách skin chưa mở

    void Start()
    {
        LoadUnlockedSkin(); // Tải trạng thái từ PlayerPrefs

        // Tạo danh sách các skin chưa mở khóa
        for (int i = 0; i < skinToUnlock.Length; i++)
        {
            if (!skinToUnlock[i].gameObject.activeSelf)
            {
                availableIndices.Add(i);
            }
        }

        unlockSkin.onClick.AddListener(UnlockRandomSkin);
    }

    void UnlockRandomSkin()
    {
        if (availableIndices.Count == 0)
        {
            Debug.Log("✅ Tất cả Skin đã được mở khóa!");
            return;
        }


        // Mở khóa button ngẫu nhiên
        int randomIndex = Random.Range(0, availableIndices.Count);
        int buttonIndex = availableIndices[randomIndex];

        skinToUnlock[buttonIndex].gameObject.SetActive(true);
        PlayerPrefs.SetInt("Skin_" + buttonIndex, 1);
        PlayerPrefs.Save();

        availableIndices.RemoveAt(randomIndex);
    }

    void LoadUnlockedSkin()
    {
        for (int i = 0; i < skinToUnlock.Length; i++)
        {
            if (PlayerPrefs.GetInt("Skin_" + i, 0) == 1)
            {
                skinToUnlock[i].gameObject.SetActive(true);
            }
            else
            {
                skinToUnlock[i].gameObject.SetActive(false);
            }
        }
    }
    public void ReloadUnlocks()
    {
        LoadUnlockedSkin();
    }

}
