using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public static Action<float> OnScored;

    [SerializeField] BasketballBar basketballBar;
    [SerializeField] PlayerAnimator playerAnimator;
    [SerializeField] Transform ballHoldPosition;
    [SerializeField] Transform targetableCheckPosition;
    [SerializeField] LayerMask targetableLayer;

    public float ShootDuration { get; private set; } = 0.6f;
    [field: SerializeField] public float ShootDistance { get; set; } = 12;

    Basketball _currentBall;
    RaycastHit[] _targetables = new RaycastHit[1];

    bool _canDoubleShoot = false;

    const float BASE_SHOOT_DURATION = 0.6f;
    const float BASE_SHOOT_DISTANCE = 12.0f;
    const int FREE_SHOOT_SPEED_MULTIPLIER = 12;

    void OnEnable() {
        
    }

    void OnDisable() {
        //OnScored -= IncreaseShooterExperience;
    }

    public void ChangeShootDuration(float changeAmount, float changeDuration) {
        StartCoroutine(ChangeShootDurationCoroutine(changeAmount, changeDuration));
    }

    public void ChangeShootDistance(float changeAmount, float changeDuration) {
        StartCoroutine(ChangeShootDistanceCoroutine(changeAmount, changeDuration));
    }

    public void StartDoubleBallPowerUp(float duration) {
        StartCoroutine(DoubleBallCoroutine(duration));
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

            StartCoroutine(LerpBallToTargetCoroutine(_currentBall, targetable, ShootDuration));

            if (_canDoubleShoot)
            {
                Basketball secondBall = basketballBar.SpawnBall(_currentBall.transform.position);
                secondBall.SetPoint(_currentBall.Point);
                StartCoroutine(LerpBallToTargetCoroutine(secondBall, targetable, ShootDuration + 0.25f));
            }

            return;
        }

        ThrowBall(_currentBall);

        if (_canDoubleShoot)
        {
            Basketball secondBall = basketballBar.SpawnBall(_currentBall.transform.position);
            secondBall.SetPoint(_currentBall.Point);
            ThrowBall(secondBall, 0.85f);
        }
    }

    void ThrowBall(Basketball ball, float speedMultiplier = 1.0f) {
        ball.StartShrinking();
        ball.transform.parent = null;
        Rigidbody ballRigidbody = ball.gameObject.AddComponent<Rigidbody>();
        ballRigidbody.AddForce(transform.forward * FREE_SHOOT_SPEED_MULTIPLIER * speedMultiplier, ForceMode.Impulse);
    }



    IEnumerator ChangeShootDurationCoroutine(float changeAmount, float changeDuration) {
        ShootDuration -= changeAmount;
        playerAnimator.IncrementAnimatorSpeed(changeAmount);
        yield return new WaitForSeconds(changeDuration);
        ShootDuration = BASE_SHOOT_DURATION;
        playerAnimator.SetAnimatorSpeed(PlayerAnimator.BASE_ANIMATOR_SPEED);
    }

    IEnumerator ChangeShootDistanceCoroutine(float changeAmount, float changeDuration) {
        ShootDistance += changeAmount;
        yield return new WaitForSeconds(changeDuration);
        ShootDistance = BASE_SHOOT_DISTANCE;
    }

    IEnumerator DoubleBallCoroutine(float duration) {
        _canDoubleShoot = true;

        yield return new WaitForSeconds(duration);

        _canDoubleShoot = false;
    }

    IEnumerator LerpBallToTargetCoroutine(Basketball ball, ITargetable targetable, float duration) {
        float _time = 0;

        Basketball ballToLerp = ball;
        ballToLerp.transform.parent = null;

        Vector3 startPosition = ballToLerp.transform.position;

        while (_time < duration)
        {
            _time += Time.deltaTime;
            float lerpAmount = _time / duration;

            if (targetable == null)
            {
                ThrowBall(ballToLerp);
                break;
            }

            Vector3 targetPosition = targetable.OnTargeted(transform);
            Vector3 movingOffset = Vector3.forward * .5f;
            Vector3 endPosition = targetable.IsMoving ? targetPosition + movingOffset : targetPosition;

            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, lerpAmount);

            Vector3 arc = Vector3.up * Mathf.Sin(lerpAmount * Mathf.PI) * targetable.ArcAmount;

            if (ballToLerp != null)
            {
                ballToLerp.transform.position = newPosition + arc;
            }

            yield return null;
        }

        if (targetable != null || ballToLerp != null)
        {
            targetable.OnReachedToTarget(ballToLerp);
        }
    }
}