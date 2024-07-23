using TMPro;
using UnityEngine;

public class CollectableBall : MonoBehaviour, ITargetable
{
    [SerializeField] Transform targetPosition;

    [SerializeField] float pointAmount = 2;

    [SerializeField] TextMeshPro pointAmountText;


    public float ArcAmount { get; private set; } = 0.8f;
    public bool IsMoving { get; private set; } = false;
    public bool IsTargeted { get; private set; }


    void Awake() {
        pointAmountText.text = pointAmount.ToString();    
    }

    public Vector3 OnTargeted(Transform player) {
        if (targetPosition == null)
            return Vector3.forward;

        return targetPosition.position;
    }

    public void OnReachedToTarget(Basketball ball) {
        pointAmount += ball.Points;
        pointAmountText.text = pointAmount.ToString();
        Destroy(ball.gameObject);
    }
}
