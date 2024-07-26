using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasketballHoop : MonoBehaviour, ITargetable
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float pointsToScore;
    [SerializeField] TextMeshPro pointText;
    [SerializeField] Vector3 scaleAnimVector = new Vector3(1.2f, 0.9f, 1f);
    [SerializeField] LayerMask defaultLayer;
    [SerializeField] bool canMove;
    [SerializeField] GameObject[] particles;

    public float ArcAmount { get; private set; } = 2.75f;
    public bool IsMoving { get; private set; } = false;
    public bool IsTargeted { get; private set; } = false;

    const float FOLLOW_PLAYER_SPEED = 7.5f;


    void Awake() {
        pointText.text = pointsToScore.ToString();
    }


    public Vector3 OnTargeted(Transform player) {
        if (!IsTargeted && canMove)
        {
            IsTargeted = true;
            StartCoroutine(FollowPlayer(player));
        }

        if (targetTransform != null)
        {
            return targetTransform.position;
        }

        return Vector3.forward;
    }

    public void OnReachedToTarget(Basketball ball) {
        if (ball == null)
            return;

        if (targetTransform == null)
            return;

        ball.transform.position = targetTransform.position;
        ball.StartShrinking();

        ScorePoint(ball);

        BounceBall(ball);

        if (pointsToScore <= 0)
        {
            OnMaxPoint();
            return;
        }

        ScoreAnimation();
    }

    void BounceBall(Basketball ball) {
        Rigidbody ballRigidbody = ball.transform.gameObject.AddComponent<Rigidbody>();
        int randomAngle = Random.Range(-30, 31);
        Vector3 bounceDirection = -transform.forward + new Vector3(randomAngle, 0, 0).normalized;
        ballRigidbody.AddForce(bounceDirection * 3.5f, ForceMode.Impulse);
    }

    void ScorePoint(Basketball ball) {
        pointsToScore -= ball.Point;
        Shooter.OnScored?.Invoke(ball.Point, SpawnParticles());
        pointText.text = pointsToScore.ToString();
    }

    GameObject[] SpawnParticles() {
        GameObject[] newParticles = new GameObject[particles.Length];

        for (int i = 0; i < particles.Length; i++)
        {
            GameObject newParticle = Instantiate(particles[i], particles[i].transform.position, Quaternion.identity);
            newParticles[i] = newParticle;
        }

        return newParticles;
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
            transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 0, 12), FOLLOW_PLAYER_SPEED * Time.deltaTime);
            followDuration -= Time.deltaTime;
            yield return null;
        }

        IsMoving = false;
    }
}