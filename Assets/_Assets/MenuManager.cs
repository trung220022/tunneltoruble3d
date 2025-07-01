using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject highScorePanel;

    void Start()
    {
        if (PlayerPrefs.GetInt("ShowHighscoreOnMenu", 0) == 1)
        {
            highScorePanel.SetActive(true); // M? b?ng ?i?m n?u c� c?
            PlayerPrefs.SetInt("ShowHighscoreOnMenu", 0); // Reset c?
        }
        else
        {
            highScorePanel.SetActive(false); // ?n n?u kh�ng c� y�u c?u
        }
    }
}
