using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] BasketballBar basketballBar;
    [SerializeField] PlayerAnimator playerAnimator;
    [SerializeField] Transform ballHoldPosition;
    [SerializeField] Transform targetableCheckPosition;
    [SerializeField] LayerMask targetableLayer;
    public float ShootDuration { get; private set; } = 0.6f;
    [field: SerializeField] public float ShootDistance { get; set; } = 12;

    Basketball _currentBall;
    RaycastHit[] _targetables = new RaycastHit[1];

    const float BASE_SHOOT_DURATION = 0.6f;
    const int FREE_SHOOT_SPEED_MULTIPLIER = 12;

    public void ChangeShootDuration(float changeAmount, float changeDuration) {
        StartCoroutine(ChangeShootDurationCoroutine(changeAmount, changeDuration));
    }

    void GetBall() {
        _currentBall = basketballBar.GetBasketball(ballHoldPosition);
        _currentBall.transform.DOLocalMove(Vector3.zero, 0.18f / PlayerAnimator.BASE_ANIMATOR_SPEED);
    }

    void Shoot() {
        int hits = Physics.RaycastNonAlloc(targetableCheckPosition.position, targetableCheckPosition.forward, _targetables, ShootDistance, targetableLayer);

        if (hits > 0)
        {
            ITargetable targetable = _targetables[0].transform.gameObject.GetComponent<ITargetable>();
            StartCoroutine(LerpBallToTargetCoroutine(targetable));
            return;
        }

        ThrowBall(_currentBall);
    }

    void ThrowBall(Basketball ball) {
        ball.StartShrinking();
        ball.transform.parent = null;
        Rigidbody ballRigidbody = ball.gameObject.AddComponent<Rigidbody>();
        ballRigidbody.AddForce(transform.forward * FREE_SHOOT_SPEED_MULTIPLIER, ForceMode.Impulse);
    }

    IEnumerator ChangeShootDurationCoroutine(float changeAmount, float changeDuration) {
        ShootDuration -= changeAmount;
        playerAnimator.IncrementAnimatorSpeed(changeAmount);
        yield return new WaitForSeconds(changeDuration);
        ShootDuration = BASE_SHOOT_DURATION;
        playerAnimator.SetAnimatorSpeed(PlayerAnimator.BASE_ANIMATOR_SPEED);
    }

    IEnumerator LerpBallToTargetCoroutine(ITargetable targetable) {
        float _time = 0;

        Basketball ball = _currentBall;
        _currentBall.transform.parent = null;

        Vector3 startPosition = ball.transform.position;

        while (_time < ShootDuration)
        {
            _time += Time.deltaTime;
            float lerpAmount = _time / ShootDuration;

            if (targetable == null)
            {
                ThrowBall(ball);
                break;
            }

            Vector3 targetPosition = targetable.OnTargeted(transform);
            Vector3 movingOffset = Vector3.forward * .5f;
            Vector3 endPosition = targetable.IsMoving ? targetPosition + movingOffset : targetPosition;

            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, lerpAmount);

            Vector3 arc = Vector3.up * Mathf.Sin(lerpAmount * Mathf.PI) * targetable.ArcAmount;

            if (ball != null)
            {
                ball.transform.position = newPosition + arc; 
            }

            yield return null;
        }

        if (targetable != null || ball != null)
        {
            targetable.OnReachedToTarget(ball);
        }
    }
}