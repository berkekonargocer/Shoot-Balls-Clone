using TMPro;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, ITargetable
{
    [SerializeField] Transform targetTransform;
    [SerializeField] TextMeshPro speedAmountText;
    [SerializeField] float powerUpAmount = 2.0f;
    [SerializeField] ParticleSystem hitParticleEffect;

    public float ArcAmount { get; private set; } = 1.0f;
    public bool IsMoving { get; private set; } = false;
    public bool IsTargeted { get; private set; }


    public Vector3 OnTargeted(Transform player) {
        return targetTransform.position;
    }

    public void OnReachedToTarget(Basketball ball) {
        powerUpAmount += ball.Points;
        speedAmountText.text = $"+{powerUpAmount}";
        hitParticleEffect.Play();
        Destroy(ball.gameObject);
    }
}
