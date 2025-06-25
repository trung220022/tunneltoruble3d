using UnityEngine;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    public GameObject panel;

    private static Stack<GameObject> panelHistory = new Stack<GameObject>();
    private static GameObject currentPanel = null;

    public void ShowPanel()
    {
        if (currentPanel != null && currentPanel != panel)
        {
            currentPanel.SetActive(false);
            panelHistory.Push(currentPanel);
        }

        panel.SetActive(true);
        currentPanel = panel;

        // 🔧 Thêm đoạn sau để reload trạng thái skin đã mở:
        UnlockSkin unlockButton = FindObjectOfType<UnlockSkin>();
        if (unlockButton != null)
        {
            unlockButton.ReloadUnlocks();
        }
    }

    public void Back()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        if (panelHistory.Count > 0)
        {
            currentPanel = panelHistory.Pop();
            currentPanel.SetActive(true);
        }
        else
        {
            currentPanel = null;
        }
    }

    public void ClosePanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            if (currentPanel == panel)
                currentPanel = null;
        }
    }
}
