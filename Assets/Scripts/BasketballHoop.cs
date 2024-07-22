using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballHoop : MonoBehaviour, ITargetable
{
    [SerializeField] Transform targetTransform;

    float _time = 0;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTargeted(Basketball ball, float shootSpeed) {
        ball.transform.parent = null;
        _time = 0;
        StartCoroutine(LerpToTargetCoroutine(ball, shootSpeed));
    }


    IEnumerator LerpToTargetCoroutine(Basketball ball, float shootSpeed) {
        float duration = .5f; // You can adjust this duration for the desired speed
        float _time = 0; // Reset time at the start of the coroutine

        Vector3 startPosition = ball.transform.position;
        Vector3 targetPosition = targetTransform.position;

        while (_time < duration)
        {
            _time += Time.deltaTime;
            float lerpAmount = _time / duration;

            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, lerpAmount);
            Vector3 arc = Vector3.up * Mathf.Sin(lerpAmount * Mathf.PI) * 2; // Adjust arc height with shootSpeed
            ball.transform.position = newPosition + arc;

            yield return null;
        }

        ball.transform.position = targetTransform.position;

        Rigidbody ballRigidbody = ball.AddComponent<Rigidbody>();
    }
}
