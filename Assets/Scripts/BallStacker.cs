using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BallStacker : MonoBehaviour
{
    [SerializeField] Transform[] stackPoints = new Transform[3];

    [SerializeField] List<CollectableBall> ballList;

    public void AddToStack(CollectableBall ball) {
        MoveInArc(ball.transform, stackPoints[ballList.Count].position, 0.35f, 0.35f);
        ballList.Add(ball);
    }

    float[] GetPointsFromBalls() {
        List<float> points = new List<float>();

        for (int i = 0; i < ballList.Count; i++)
        {
            points.Add(ballList[i].PointAmount);
        }

        return points.ToArray();
    }

    void MoveInArc(Transform objectToMove, Vector3 targetPosition, float duration, float arcHeight) {
        Vector3 midPoint = (objectToMove.position + targetPosition) / 2 + Vector3.up * arcHeight;

        Vector3[] path = new Vector3[] { objectToMove.position, midPoint, targetPosition };

        objectToMove.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.Linear);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            BasketballBar bar = other.transform.gameObject.GetComponentInChildren<BasketballBar>();
            bar.SetBallPoints(GetPointsFromBalls());

            for (int i = 0;i < ballList.Count; i++)
            {
                MoveInArc(ballList[i].transform, bar.BallPositions[3].position, 0.25f, 0.75f);
                ballList[i].transform.DOScale(0, 0.25f);
                Destroy(ballList[i].gameObject, 0.5f);
            }

            ballList.Clear();
        }
    }
}