using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("In-Game UI")]
    public TextMeshProUGUI scoreText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI bestScoreText;

    [Header("Tap To Play")]
    public GameObject tapToPlayText;

    void Awake()
    {
        instance = this;

        // مهم جدًا: نخفي Game Over عند بداية اللعبة
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (tapToPlayText != null)
            tapToPlayText.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void ShowGameOver(int finalScore, int bestScore)
    {
        gameOverPanel.SetActive(true);
        tapToPlayText.SetActive(true);

        finalScoreText.text = "Oops!\nGame Over";
        bestScoreText.text = "Best Score:\n" + bestScore;
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
        tapToPlayText.SetActive(false);
    }

    public void ShowTapToPlay()
    {
        tapToPlayText.SetActive(true);
    }

    public void HideTapToPlay()
    {
        tapToPlayText.SetActive(false);
    }
}
