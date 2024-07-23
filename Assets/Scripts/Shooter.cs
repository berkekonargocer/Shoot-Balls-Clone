using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] BasketballBar basketballBar;
    [SerializeField] Transform ballHoldPosition;
    [SerializeField] Transform targetableCheckPosition;
    [SerializeField] LayerMask targetableLayer;

    [field: SerializeField] public float ShootDistance { get; set; } = 10;
    [SerializeField] float shootSpeed = 2;

    Basketball _currentBall;
    RaycastHit[] _targetables = new RaycastHit[1];

    const int FREE_SHOOT_SPEED_MULTIPLIER = 12;
    const float HOOP_FOLLOW_SPEED = 7.5f;


    void Start() {

    }

    void Update() {
        if (_targetables[0].transform != null)
        {
            _targetables[0].transform.position = Vector3.Lerp(_targetables[0].transform.position, transform.position + new Vector3(0, 0, 10), HOOP_FOLLOW_SPEED * Time.deltaTime);
        }
    }

    void GetBall() {
        _currentBall = basketballBar.GetBasketball(ballHoldPosition);
        _currentBall.transform.DOLocalMove(Vector3.zero, 0.18f);
    }

    void Shoot() {
        int hits = Physics.RaycastNonAlloc(targetableCheckPosition.position, targetableCheckPosition.forward, _targetables, ShootDistance, targetableLayer);

        if (hits > 0)
        {
            ITargetable targetable = _targetables[0].transform.gameObject.GetComponent<ITargetable>();
            StartCoroutine(LerpBallToTargetCoroutine(targetable));
            return;
        }

        _currentBall.transform.parent = null;
        Rigidbody ballRigidbody = _currentBall.gameObject.AddComponent<Rigidbody>();
        ballRigidbody.AddForce(transform.forward * shootSpeed * FREE_SHOOT_SPEED_MULTIPLIER, ForceMode.Impulse);
    }

    IEnumerator LerpBallToTargetCoroutine(ITargetable targetable) {
        float duration = .5f;
        float _time = 0;

        Basketball ball = _currentBall;
        _currentBall.transform.parent = null;

        Vector3 startPosition = ball.transform.position;

        while (_time < duration)
        {
            _time += Time.deltaTime;
            float lerpAmount = _time / duration;
            Vector3 targetPosition = targetable.OnTargeted(transform);
            Vector3 movingOffset = Vector3.forward * .5f;
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition + movingOffset, lerpAmount);
            Vector3 arc = Vector3.up * Mathf.Sin(lerpAmount * Mathf.PI) * 2.4f;
            ball.transform.position = newPosition + arc;

            yield return null;
        }

        targetable.OnReachedToTarget(ball);
    }
}
