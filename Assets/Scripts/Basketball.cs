using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    [SerializeField] TextMeshPro pointsText;
    public float Point { get; private set; } = 0.5f;

    public void StartShrinking() {
        StartCoroutine(ShrinkCoroutine());
    }

    public void SetPoint(float point) {
        Point = point;
        SetPointText();
    }

    IEnumerator ShrinkCoroutine() {
        yield return new WaitForSeconds(2.0f);
        transform.DOScale(0, 0.35f);
        Destroy(gameObject, 0.4f);
    }

    void SetPointText() {
        pointsText.text = Point.ToString();
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}