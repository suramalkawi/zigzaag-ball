using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] public TMP_Text currentScoreText;
    [SerializeField] public TMP_Text bestScoreText;

    public int currentScore = 0;
    public int bestScore = 0;

    private void Awake()
    {
        // عشان يظل نسخة واحدة من ScoreManager
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // تحميل أفضل نتيجة من PlayerPrefs
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        UpdateUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentScoreText != null)
            currentScoreText.text = "Score: " + currentScore;
        if (bestScoreText != null)
            bestScoreText.text = "Best: " + bestScore;
    }
}
