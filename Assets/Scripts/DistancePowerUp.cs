using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DistancePowerUp : MonoBehaviour, ITargetable
{
    [SerializeField] float powerUpDuration = 5.0f;
    [SerializeField] Transform targetTransform;
    [SerializeField] TextMeshPro distanceAmountText;
    [SerializeField] float powerUpAmount = 1.0f;
    [SerializeField] ParticleSystem hitParticleEffect;

    public float ArcAmount { get; } = 1.0f;
    public bool IsMoving { get; } = false;
    public bool IsTargeted { get; }

    void Awake() {
        distanceAmountText.text = "+" + powerUpAmount.ToString();
    }

    public Vector3 OnTargeted(Transform player) {
        if (targetTransform != null)
        {
            return targetTransform.position;
        }

        return Vector3.forward;
    }

    public void OnReachedToTarget(Basketball ball) {
        powerUpAmount += ball.Point;
        distanceAmountText.text = $"+{powerUpAmount}";

        if (hitParticleEffect != null)
        {
            hitParticleEffect.Play();
        }

        if (ball != null)
        {
            Destroy(ball.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Shooter>().ChangeShootDistance(powerUpAmount, powerUpDuration);
            transform.DOScale(0, 0.15f);
            Destroy(gameObject, 0.2f);
        }
    }
}
