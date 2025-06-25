using UnityEngine;
using UnityEngine.UI;

public class SkinButtonSelector : MonoBehaviour
{
    public int skinIndex; // Gán đúng số thứ tự của skin trong Inspector
    public GameObject tickIcon;

    private static SkinButtonSelector selectedButton = null;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        CheckIfSelected();
    }

    public void OnClick()
    {
        if (selectedButton != null && selectedButton != this)
        {
            selectedButton.ShowTick(false);
        }

        selectedButton = this;
        ShowTick(true);

        PlayerPrefs.SetInt("SkinChoise", skinIndex); // ✅ Dùng skinIndex đúng
        PlayerPrefs.Save();

        Debug.Log("Skin đã chọn: " + skinIndex);
    }

    public void ShowTick(bool show)
    {
        if (tickIcon != null)
            tickIcon.SetActive(show);
    }

    public void CheckIfSelected()
    {
        int savedIndex = PlayerPrefs.GetInt("SkinChoise", -1);
        if (savedIndex == skinIndex)
        {
            selectedButton = this;
            ShowTick(true);
        }
        else
        {
            ShowTick(false);
        }
    }
}
