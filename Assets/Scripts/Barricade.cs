using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour, ITargetable
{
    [SerializeField] Transform targetTransform;
    [SerializeField] LayerMask defaultLayer;
    [SerializeField] List<GameObjectArrayWrapper> barricadeList = new List<GameObjectArrayWrapper>();

    public float ArcAmount { get; }
    public bool IsMoving { get; }
    public bool IsTargeted { get; }

    public Vector3 OnTargeted(Transform player) {
        if (targetTransform != null)
            return targetTransform.position;

        return Vector3.zero;
    }

    public void OnReachedToTarget(Basketball ball) {
        BounceBall(ball);

        foreach (GameObject item in barricadeList[0].gameObjects)
        {
            item.AddComponent<Rigidbody>();
            Destroy(item, 2.5f);

        }

        barricadeList.RemoveAt(0);

        if (barricadeList.Count <= 0)
        {
            gameObject.layer = defaultLayer;
            gameObject.GetComponent<Collider>().enabled = false;
            return;
        }
    }

    void BounceBall(Basketball ball) {
        Rigidbody ballRigidbody = ball.transform.gameObject.AddComponent<Rigidbody>();
        int randomAngle = Random.Range(-30, 31);
        Vector3 bounceDirection = -transform.forward + new Vector3(randomAngle, 0, 0).normalized;
        ballRigidbody.AddForce(bounceDirection * 3.5f, ForceMode.Impulse);
    }
}