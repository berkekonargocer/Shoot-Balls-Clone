using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    [SerializeField] TextMeshPro pointsText;
    public float Points { get; private set; } = 0.5f;

    public void StartShrinking() {
        StartCoroutine(ShrinkCoroutine());
    }

    public void SetPoints(float points) {
        Points = points;
        SetPointsText();
    }

    IEnumerator ShrinkCoroutine() {
        yield return new WaitForSeconds(2.0f);
        transform.DOScale(0, 0.35f);
        Destroy(gameObject, 0.4f);
    }

    void SetPointsText() {
        pointsText.text = Points.ToString();
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
