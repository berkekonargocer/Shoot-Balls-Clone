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

    public List<CollectableBall> GetStack() {
        return ballList;
    }

    void MoveInArc(Transform objectToMove, Vector3 targetPosition, float duration, float arcHeight) {
        Vector3 midPoint = (objectToMove.position + targetPosition) / 2 + Vector3.up * arcHeight;

        Vector3[] path = new Vector3[] { objectToMove.position, midPoint, targetPosition };

        objectToMove.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.Linear);
    }
}