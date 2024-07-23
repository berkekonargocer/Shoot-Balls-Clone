using TMPro;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    [SerializeField] TextMeshPro pointsText;
    public float Points { get; private set; } = 0.5f;

    public void SetPoints(float points) {
        Points = points;
        SetPointsText();
    }

    void SetPointsText() {
        pointsText.text = Points.ToString();
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
