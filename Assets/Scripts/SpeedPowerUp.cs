using TMPro;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, ITargetable
{
    [SerializeField] Transform targetTransform;
    [SerializeField] TextMeshPro speedAmountText;
    [SerializeField] float powerUpAmount = 2.0f;

    public bool IsTargeted { get; private set; }

    public Vector3 OnTargeted(Transform player) {
        return targetTransform.position;
    }

    public void OnReachedToTarget(Basketball ball) {
        powerUpAmount += ball.Points;
        speedAmountText.text = $"+{powerUpAmount}";
        Destroy(ball.gameObject);
    }
}
