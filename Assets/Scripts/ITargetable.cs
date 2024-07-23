using UnityEngine;

public interface ITargetable
{
    public bool IsTargeted { get; }
    public Vector3 OnTargeted(Transform player);
    public void OnReachedToTarget(Basketball ball);
}
