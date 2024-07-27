using TMPro;
using UnityEngine;

public class LevelCompletedPanel : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI moneyText;

    void OnEnable() {
        GameManager.Instance.OnLoseGame += EnableLevelCompletedPanel;
    }

    void OnDisable() {
        GameManager.Instance.OnLoseGame -= EnableLevelCompletedPanel;
    }

    void EnableLevelCompletedPanel(float score) {
        panel.gameObject.SetActive(true);
        moneyText.text = $"{score} $";
    }
}
