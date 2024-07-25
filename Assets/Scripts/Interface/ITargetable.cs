using UnityEngine;

public interface ITargetable
{
    public float ArcAmount { get; }
    public bool IsMoving { get; }
    public bool IsTargeted { get; }
    public Vector3 OnTargeted(Transform player);
    public void OnReachedToTarget(Basketball ball);
}
