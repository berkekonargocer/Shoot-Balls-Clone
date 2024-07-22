using DG.Tweening;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] BasketballBar basketballBar;
    [SerializeField] Transform ballHoldPosition;
    [SerializeField] Transform targetableCheckPosition;
    [SerializeField] LayerMask targetableLayer;

    [SerializeField] float shootSpeed = 2;
    const int FREE_SHOOT_SPEED_MULTIPLIER = 12;
    [field: SerializeField] public float ShootDistance { get; set; } = 10;

    Basketball _currentBall;
    RaycastHit[] _targetables = new RaycastHit[1];

    void Start() {

    }

    void Update() {

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
            targetable.OnTargeted(_currentBall, shootSpeed);
            return;
        }

        _currentBall.transform.parent = null;
        Rigidbody ballRigidbody = _currentBall.gameObject.AddComponent<Rigidbody>();
        ballRigidbody.AddForce(transform.forward * shootSpeed * FREE_SHOOT_SPEED_MULTIPLIER, ForceMode.Impulse);
    }
}
