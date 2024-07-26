using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShooterLevelUI : MonoBehaviour
{
    [SerializeField] Image shooterLevelSlider;

    [SerializeField] RectTransform movePosition;
    [SerializeField] Canvas uiCanvas;


    void OnEnable() {
        Shooter.OnShooterExperienceChanged += UpdateShooterLevelUI;
    }

    void OnDisable() {
        Shooter.OnShooterExperienceChanged -= UpdateShooterLevelUI;
    }


    void UpdateShooterLevelUI(float points, GameObject[] particles) {
        for (int i = 0; i < particles.Length; i++)
        {
            MakeUIElement(particles[i]);
            particles[i].transform.DOMove(movePosition.position, 1.0f);
            Destroy(particles[i], 1.05f);
        }

        shooterLevelSlider.fillAmount = points;
    }

    void MakeUIElement(GameObject worldElement) {
        RectTransform rectTransform = worldElement.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;

        rectTransform.transform.SetParent(uiCanvas.transform);
    }
}