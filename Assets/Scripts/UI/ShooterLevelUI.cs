using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShooterLevelUI : MonoBehaviour
{
    [SerializeField] Image shooterLevelSlider;

    [SerializeField] RectTransform movePositionRectTransform;
    [SerializeField] Canvas uiCanvas;


    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.OnShooterExperienceChanged += UpdateShooterLevelUI;
    }

    void OnDisable() {
        GameManager.OnShooterExperienceChanged -= UpdateShooterLevelUI;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        SetShooterLevelUI();
    }

    void SetShooterLevelUI() {
        shooterLevelSlider.fillAmount = GameManager.Instance.ShooterExperience;
    }

    void UpdateShooterLevelUI(float points) {
        //for (int i = 0; i < particles.Length; i++)
        //{
        //    GameObject particle = particles[i];
        //    particle.transform.DOScale(6, 1.0f);
        //    particle.transform.DOMove(movePositionRectTransform.transform.position, 1.0f).OnComplete(() => Destroy(particle));
        //}

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