using UnityEngine;

public class FollowTargetZ : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] float followOffsetZ;
    [SerializeField] bool autoSceneOffset;


    void Awake() {
        if (autoSceneOffset)
        {
            followOffsetZ = -(followTarget.position.z - transform.position.z);
        }
    }

    void LateUpdate()
    {
        float followPositionZ = followTarget.position.z + followOffsetZ;
        transform.position = new Vector3(transform.position.x, transform.position.y, followPositionZ);
    }
}
