using DG.Tweening;
using TMPro;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, ITargetable
{

    //[SerializeField] float powerUpDuration = 5.0f;
    [SerializeField] Transform targetTransform;
    [SerializeField] TextMeshPro speedAmountText;
    [SerializeField] float powerUpAmount = 15.0f;
    [SerializeField] ParticleSystem hitParticleEffect;

    public float ArcAmount { get; private set; } = 1.0f;
    public bool IsMoving { get; private set; } = false;
    public bool IsTargeted { get; private set; }

    void Awake() {
        speedAmountText.text = "+" + powerUpAmount.ToString();    
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Shooter>().ChangeShootDuration(powerUpAmount / 100);
            transform.DOScale(0, 0.15f);
            Destroy(gameObject, 0.2f);
        }
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
        speedAmountText.text = $"+{powerUpAmount}";

        if (hitParticleEffect != null)
        {
            hitParticleEffect.Play(); 
        }

        if (ball != null)
        {
            Destroy(ball.gameObject); 
        }
    }
}
