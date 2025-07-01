using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text readyGoText;
    public Text scoreText;

    [HideInInspector]
    public bool isGameStarted = false;

    [Header("Tên dùng để lưu điểm (tự gán theo scene)")]
    public string sceneKey = "Level1"; // 👈 Kéo gán tay trong Inspector

    [Header("Thời gian hiển thị (giây)")]
    public float readyTime = 1f;
    public float goTime = 1f;

    private int score = 0;

    [Header("Panel Thua Game")]
    public GameObject gameOverPanel;
    public Text finalScoreText;
    public Text highScoreText;

    private float scoreTimer = 0f;
    public float scoreInterval = 0.25f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        StartCoroutine(ReadyGoSequence());
    }

    IEnumerator ReadyGoSequence()
    {
        isGameStarted = false;
        score = 0;
        scoreTimer = 0f;
        UpdateScoreText();

        readyGoText.gameObject.SetActive(true);
        readyGoText.text = "Ready!";
        yield return new WaitForSeconds(readyTime);

        readyGoText.text = "Go!";
        yield return new WaitForSeconds(goTime);

        readyGoText.gameObject.SetActive(false);
        isGameStarted = true;
    }

    private void Update()
    {
        if (!isGameStarted) return;

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreInterval)
        {
            int pointsToAdd = Mathf.FloorToInt(scoreTimer / scoreInterval);
            AddScore(pointsToAdd);
            scoreTimer -= pointsToAdd * scoreInterval;
        }
    }

    public void AddScore(int amount)
    {
        if (!isGameStarted) return;

        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        isGameStarted = false;

        string highScoreKey = "HighScore_" + sceneKey; // 👈 Dùng key gán thủ công

        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save(); // 👈 Ép lưu
        }

        if (finalScoreText != null)
            finalScoreText.text = "" + score;

        if (highScoreText != null)
            highScoreText.text = "" + highScore;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void LoadMenuAndShowHighscore()
    {
        PlayerPrefs.SetInt("ShowHighscoreOnMenu", 1); // Ghi cờ để hiện bảng điểm
        SceneManager.LoadScene("MainScene");          // Đổi tên nếu menu bạn khác
    }
}
