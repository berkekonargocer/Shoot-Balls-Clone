using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballBar : MonoBehaviour
{
    [SerializeField] GameObject basketballPrefab;

    [SerializeField] float reorderDuration = 0.1f;

    [SerializeField] float instantiateBallScaleDuration = 0.7f;

    [SerializeField] Transform[] ballPositions;
    [SerializeField] List<Basketball> basketballs = new List<Basketball>();

    [field: SerializeField] public float[] BallPoints { get; set; } = { 0.5f, 0.5f, 0.5f, 0.5f};
    int _currentSpawnIndex = 0;

    
    public void SetBallPoints(float[] points) {
        float[] newPoints = new float[points.Length + BallPoints.Length];
        Array.Sort(points);
        

        for (int i = 0; i < BallPoints.Length; i++)
        {
            if (BallPoints[i] < points[i])
            {
                BallPoints[i] = points[i];
            }
        }
    }

    public Basketball GetBasketball(Transform parent) {
        Basketball ball = basketballs[0];
        ball.transform.SetParent(parent);
        basketballs.RemoveAt(0);
        SpawnNewBall();

        return ball;
    }

    void SpawnNewBall() {
        Basketball newBall = Instantiate(basketballPrefab, ballPositions[3].transform.position, Quaternion.Euler(new Vector3(0, 180, 0)), ballPositions[3].transform).GetComponent<Basketball>();
        newBall.SetPoints(BallPoints[_currentSpawnIndex]);
        basketballs.Add(newBall);

        if (_currentSpawnIndex < BallPoints.Length - 1)
        {
            _currentSpawnIndex++;
        }
        else
        {
            _currentSpawnIndex = 0;
        }

        SpawnAnimation(newBall);
    }

    private void SpawnAnimation(Basketball newBall) {
        newBall.transform.localScale = Vector3.zero;
        newBall.transform.DOScale(new Vector3(0.875f, 0.875f, 0.875f), instantiateBallScaleDuration).OnComplete(ReorderBalls);
    }

    void ReorderBalls() {
        for (int i = 0; i < basketballs.Count; i++)
        {
            basketballs[i].transform.SetParent(ballPositions[i].transform);
            basketballs[i].transform.DOLocalMove(Vector3.zero, reorderDuration);
        }
    }
}
