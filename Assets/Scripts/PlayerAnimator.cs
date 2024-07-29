using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimator : MonoBehaviour
{
    Animator _playerAnimator;
    public const float BASE_ANIMATOR_SPEED = 0.95f;

    void OnEnable() {
        SetAnimatorSpeed(0.95f);
        GameManager.Instance.OnLoseGame += PlayLoseGameAnimation;
        GameManager.Instance.OnStartGame += PlayMovingAnimation;
    }

    void OnDisable() {
        GameManager.Instance.OnLoseGame -= PlayLoseGameAnimation;
        GameManager.Instance.OnStartGame -= PlayMovingAnimation;
    }

    void Awake() {
        _playerAnimator = GetComponent<Animator>();
    }

    public void SetAnimatorSpeed(float speed) {
        _playerAnimator.speed = speed;
    }

    public void IncrementAnimatorSpeed(float incrementAmount) {
        _playerAnimator.speed += incrementAmount;
    }

    void PlayMovingAnimation() {
        _playerAnimator?.SetBool("IsMoving", true);
    }

    void PlayLoseGameAnimation(float score) {
        _playerAnimator?.SetBool("IsMoving", false);
    }
}