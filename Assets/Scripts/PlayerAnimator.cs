using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimator : MonoBehaviour
{
    Animator _playerAnimator;
    public const float BASE_ANIMATOR_SPEED = 0.95f;

    void OnEnable() {
        SetAnimatorSpeed(0.95f);
        //GameManager.Instance.OnWinGame += PlayWinGameAnimation;
        GameManager.Instance.OnLoseGame += PlayLoseGameAnimation;
        GameManager.Instance.OnStartGame += PlayMovingAnimation;
    }

    void OnDisable() {
        //GameManager.Instance.OnWinGame -= PlayWinGameAnimation;
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

    public void PlayPointCollectAnimation() {
        _playerAnimator?.SetTrigger("pointCollected");
    }

    public void PlayJumpAnimation() {
        _playerAnimator.SetBool("isJumping", true);
    }

    void StopJumpAnimation() {
        _playerAnimator.SetBool("isJumping", false);
    }

    void SetItemAmountParameter(int itemAmount) {
        _playerAnimator.SetInteger("itemAmount", itemAmount);
    }

    void PlayMovingAnimation() {
        _playerAnimator?.SetBool("IsMoving", true);
    }

    void PlayWinGameAnimation(int score) {
        SetItemAmountParameter(0);
        Vector3 lookRotation = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        transform.LookAt(lookRotation);

        _playerAnimator?.SetBool("hasWon", true);
    }

    void PlayLoseGameAnimation(float score) {
        _playerAnimator?.SetBool("IsMoving", false);

        //SetItemAmountParameter(0);
        //Vector3 lookRotation = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        //transform.LookAt(lookRotation);
        //_playerAnimator?.SetBool("hasLost", true);
    }
}