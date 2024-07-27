using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShooterLevelUI : MonoBehaviour
{
    [SerializeField] Image shooterLevelSlider;

    [SerializeField] RectTransform movePositionRectTransform;
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
            GameObject particle = particles[i];
            particle.transform.DOScale(6, 1.0f);
            particle.transform.DOMove(movePositionRectTransform.transform.position, 1.0f).OnComplete(() => Destroy(particle));
        }

        shooterLevelSlider.fillAmount = points;
    }

    Vector3 GetCanvasPosition(GameObject worldElement) {
        ///TEST CASE 1
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldElement.transform.position);

        RectTransform canvasRect = uiCanvas.gameObject.GetComponent<RectTransform>();
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, uiCanvas.worldCamera, out canvasPosition);
        return canvasPosition;
    }
}