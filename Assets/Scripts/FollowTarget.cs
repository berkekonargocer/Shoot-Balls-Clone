using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] Vector3 followOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float followPositionZ = followTarget.position.z + followOffset.z;
        transform.position = new Vector3(transform.position.x, transform.position.y, followPositionZ);
    }
}
