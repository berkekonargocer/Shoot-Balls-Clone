using UnityEngine;

public class DoubleBallPowerUp : MonoBehaviour, ITargetable
{
    public float ArcAmount { get; }
    public bool IsMoving { get; }
    public bool IsTargeted { get; }

    public Vector3 OnTargeted(Transform player) {
        throw new System.NotImplementedException();
    }

    public void OnReachedToTarget(Basketball ball) {
        throw new System.NotImplementedException();
    }
}