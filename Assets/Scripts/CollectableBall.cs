using DG.Tweening;
using TMPro;
using UnityEngine;

public class CollectableBall : MonoBehaviour, ITargetable
{
    [SerializeField] Transform targetPosition;

    [SerializeField] ParticleSystem onHitParticleEffect;

    [field: SerializeField] public float PointAmount { get; private set; } = 2;

    [SerializeField] TextMeshPro pointAmountText;

    [SerializeField] LayerMask defaultLayer;

    public float ArcAmount { get; private set; } = 0.8f;
    public bool IsMoving { get; private set; } = false;
    public bool IsTargeted { get; private set; }


    void Awake() {
        pointAmountText.text = PointAmount.ToString();
    }

    public Vector3 OnTargeted(Transform player) {
        if (targetPosition == null)
            return Vector3.forward;

        return targetPosition.position;
    }

    public void OnReachedToTarget(Basketball ball) {
        onHitParticleEffect.Play();
        PointAmount += ball.Point;
        pointAmountText.text = PointAmount.ToString();
        Destroy(ball.gameObject);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            gameObject.layer = defaultLayer;
            MoveInArc(transform, new Vector3(-3.5f, transform.position.y, transform.position.z + 2.5f), 0.5f, 1.0f);
        }

        if (other.CompareTag("BallStacker"))
        {
            other.transform.gameObject.GetComponent<BallStacker>().AddToStack(this);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("BallWay"))
        {
            transform.position += Vector3.forward * 0.25f;
        }
    }

    void MoveInArc(Transform objectToMove, Vector3 targetPosition, float duration, float arcHeight) {
        Vector3 midPoint = (objectToMove.position + targetPosition) / 2 + Vector3.up * arcHeight;

        Vector3[] path = new Vector3[] { objectToMove.position, midPoint, targetPosition };

        objectToMove.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.Linear);
    }
}