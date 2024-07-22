using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimator : MonoBehaviour
{
    Animator _playerAnimator;


    void OnEnable() {
        //GameManager.Instance.OnWinGame += PlayWinGameAnimation;
        //GameManager.Instance.OnLoseGame += PlayLoseGameAnimation;
        GameManager.Instance.OnStartGame += PlayMovingAnimation;
    }

    void OnDisable() {
        //GameManager.Instance.OnWinGame -= PlayWinGameAnimation;
        //GameManager.Instance.OnLoseGame -= PlayLoseGameAnimation;
        GameManager.Instance.OnStartGame -= PlayMovingAnimation;
    }

    void Awake() {
        _playerAnimator = GetComponent<Animator>();
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

    void PlayLoseGameAnimation(int score) {
        SetItemAmountParameter(0);
        Vector3 lookRotation = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        transform.LookAt(lookRotation);
        _playerAnimator?.SetBool("hasLost", true);
    }
}