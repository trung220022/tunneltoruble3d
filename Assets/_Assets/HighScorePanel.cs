using UnityEngine;
using UnityEngine.UI;

public class HighScorePanel : MonoBehaviour
{
    public Text level1Text;
    public Text level2Text;
    public Text level3Text;

    void Start()
    {
        ShowAllHighScores(); // Hiển thị khi bắt đầu
    }

    void OnEnable()
    {
        ShowAllHighScores(); // Hiển thị lại mỗi khi Panel được bật
    }

    public void ShowAllHighScores()
    {
        int score1 = PlayerPrefs.GetInt("HighScore_Level1", 0);
        int score2 = PlayerPrefs.GetInt("HighScore_Level2", 0);
        int score3 = PlayerPrefs.GetInt("HighScore_Level3", 0);

        level1Text.text = score1.ToString(); // ❌ không có chữ "Level 1:"
        level2Text.text = score2.ToString();
        level3Text.text = score3.ToString();
    }
}
