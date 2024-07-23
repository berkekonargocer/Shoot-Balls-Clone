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

    public float ArcAmount { get; private set; } = 2.75f;
    public bool IsMoving { get; private set; } = false;
    public bool IsTargeted { get; private set; } = false;

    const float FOLLOW_PLAYER_SPEED = 7.5f;


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
        ball.StartShrinking();

        Rigidbody ballRigidbody = ball.AddComponent<Rigidbody>();

        BounceBall(ballRigidbody);

        ScorePoint(ball);

        if (pointsToScore <= 0)
        {
            OnMaxPoint();
            return;
        }

        ScoreAnimation();
    }

    void BounceBall(Rigidbody ballRigidbody) {
        int randomAngle = Random.Range(-30, 31);
        Vector3 bounceDirection = -transform.forward + new Vector3(randomAngle, 0, 0).normalized;
        ballRigidbody.AddForce(bounceDirection * 3.5f, ForceMode.Impulse);
    }

    void ScorePoint(Basketball ball) {
        pointsToScore -= ball.Points;
        pointText.text = pointsToScore.ToString();
    }

    void ScoreAnimation() {
        transform.DOScale(scaleAnimVector, 0.1f).OnComplete(() => transform.DOScale(Vector3.one, 0.1f));
    }

    void OnMaxPoint() {
        gameObject.layer = defaultLayer;
        pointsToScore = 0;
        pointText.text = pointsToScore.ToString();
        transform.DOScale(0, 0.2f);
        Destroy(gameObject, 0.25f);
    }

    IEnumerator FollowPlayer(Transform player) {
        float followDuration = 4.0f;

        IsMoving = true;

        while (followDuration > 0)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 0, 10), FOLLOW_PLAYER_SPEED * Time.deltaTime);
            followDuration -= Time.deltaTime;
            yield return null;
        }

        IsMoving = false;
    }
}
