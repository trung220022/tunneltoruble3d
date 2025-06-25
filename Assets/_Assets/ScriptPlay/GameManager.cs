using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text readyGoText; // Text "Ready" và "Go"
    public Text scoreText;   // Text hiển thị điểm

    [HideInInspector]
    public bool isGameStarted = false; // Trạng thái bắt đầu game

    [Header("Thời gian hiển thị (giây)")]
    public float readyTime = 1f;       // Thời gian hiển thị "Ready"
    public float goTime = 1f;          // Thời gian hiển thị "Go"

    private int score = 0;             // Điểm hiện tại

    [Header("Panel Thua Game")]
    public GameObject gameOverPanel;  // Panel khi thua
    public Text finalScoreText;       // Text điểm cuối cùng
    public Text highScoreText;        // Text điểm cao nhất

    private float scoreTimer = 0f;     // Bộ đếm thời gian tăng điểm
    public float scoreInterval = 0.25f; // Thời gian mỗi lần tăng điểm

    private void Awake()
    {
        if (Instance == null) Instance = this; // Singleton
        else Destroy(gameObject);

        Application.targetFrameRate = 60;      // Giới hạn FPS
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);   // Ẩn panel thua lúc đầu

        StartCoroutine(ReadyGoSequence());    // Bắt đầu chuỗi Ready-Go
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

        scoreTimer += Time.deltaTime; // Cộng thời gian
        if (scoreTimer >= scoreInterval)
        {
            int pointsToAdd = Mathf.FloorToInt(scoreTimer / scoreInterval); // Tính số điểm cần cộng
            AddScore(pointsToAdd);
            scoreTimer -= pointsToAdd * scoreInterval; // Giữ phần dư
        }
    }

    public void AddScore(int amount)
    {
        if (!isGameStarted) return;

        score += amount;              // Cộng điểm
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = score.ToString(); // Cập nhật UI điểm
    }

    public void GameOver()
    {
        isGameStarted = false;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)                  // Lưu high score mới nếu cần
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        if (finalScoreText != null)
            finalScoreText.text = "" + score;   // Cập nhật điểm cuối

        if (highScoreText != null)
            highScoreText.text = "" + highScore; // Cập nhật điểm cao nhất

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);      // Hiện panel thua
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại scene hiện tại
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainScene"); // Chuyển về scene menu
    }
}
