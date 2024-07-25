using NOJUMPO;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void OnEnable() {
        UpdateScoreUI(ScoreManager.Instance.Score);
        ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
    }

    void OnDisable() {
        ScoreManager.Instance.OnScoreChanged -= UpdateScoreUI;
    }

    void UpdateScoreUI(float score) {
        scoreText.text = $"{score:F1}";
    }
}
