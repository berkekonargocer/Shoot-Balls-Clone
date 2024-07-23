using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballHoop : MonoBehaviour, ITargetable
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float pointsToScore;
    [SerializeField] TextMeshPro pointText;
    [SerializeField] Vector3 scaleAnimVector = new Vector3(1.2f, 0.9f, 1f);

    [SerializeField] LayerMask defaultLayer;

    public bool IsTargeted { get; private set; } = false;


    public Vector3 OnTargeted(Transform player) {
        if (!IsTargeted)
        {
            IsTargeted = true;
            StartCoroutine(FollowPlayer(player));
        }
        return targetTransform.position;
    }

    public void OnReachedToTarget(Basketball ball) {
        ball.transform.position = targetTransform.position;

        Rigidbody ballRigidbody = ball.AddComponent<Rigidbody>();

        int randomAngle = Random.Range(-30, 31);
        Vector3 bounceDirection = -transform.forward + new Vector3(randomAngle, 0, 0).normalized;
        ballRigidbody.AddForce(bounceDirection * 3.5f, ForceMode.Impulse);

        pointsToScore -= ball.Points;

        if (pointsToScore <= 0)
        {
            gameObject.layer = defaultLayer;
            pointsToScore = 0;
            pointText.text = pointsToScore.ToString();
            transform.DOScale(0, 0.2f);
            Destroy(gameObject, 0.25f);
            return;
        }

        transform.DOScale(scaleAnimVector, 0.1f).OnComplete(() => transform.DOScale(Vector3.one, 0.1f));
        pointText.text = pointsToScore.ToString();
    }

    IEnumerator FollowPlayer(Transform player) {

    }
}
