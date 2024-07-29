using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BasketballBar : MonoBehaviour
{
    [SerializeField] GameObject basketballPrefab;

    [SerializeField] float reorderDuration = 0.1f;

    [SerializeField] float instantiateBallScaleDuration = 0.7f;

    [field: SerializeField] public Transform[] BallPositions { get; private set; }
    [SerializeField] List<Basketball> basketballs = new List<Basketball>();

    [field: SerializeField] public float[] BallPoints { get; set; } = { 0.5f, 0.5f, 0.5f, 0.5f};
    int _currentSpawnIndex = 0;


    void OnEnable() {
        UpgradeManager.Instance.OnBallUpgrade += SetInitialBallPoint;
    }

    void OnDisable() {
        UpgradeManager.Instance.OnBallUpgrade -= SetInitialBallPoint;

    }


    public void SetInitialBallPoint(Upgradeable upgradeable) {
        for (int i = 0; i < BallPositions.Length; i++)
        {
            BallPoints[i] = upgradeable.currentLevelValue;
        }

        RemoveAllBalls();
        RespawnInitialBalls();
    }

    public void SetBallPoints(float[] points) {
        List<float> newPointsList = new List<float>(points.Length + BallPoints.Length);
        newPointsList.AddRange(BallPoints);
        newPointsList.AddRange(points);

        newPointsList.Sort((a, b) => b.CompareTo(a));

        for (int i = 0; i < BallPoints.Length; i++)
        {
            BallPoints[i] = newPointsList[i];
        }
    }

    public Basketball SpawnBall(Vector3 position) {
        return Instantiate(basketballPrefab, position, Quaternion.Euler(new Vector3(0, 180, 0))).GetComponent<Basketball>();
    }

    public Basketball SpawnBall(Vector3 position, Transform parent) {
        return Instantiate(basketballPrefab, position, Quaternion.Euler(new Vector3(0, 180, 0)), parent).GetComponent<Basketball>();
    }

    public Basketball GetBasketball(Transform parent) {
        Basketball ball = basketballs[0];
        ball.transform.SetParent(parent);
        basketballs.RemoveAt(0);
        SpawnNewBall();

        return ball;
    }

    void RespawnInitialBalls() {
        for (int i = 0; i < BallPositions.Length; i++)
        {
            Basketball newBall = SpawnBall(BallPositions[_currentSpawnIndex].transform.position, BallPositions[_currentSpawnIndex].transform);
            newBall.SetPoint(BallPoints[_currentSpawnIndex]);
            basketballs.Add(newBall);

            if (_currentSpawnIndex < BallPoints.Length - 1)
            {
                _currentSpawnIndex++;
            }
            else
            {
                _currentSpawnIndex = 0;
            } 
        }
    }

    void SpawnNewBall() {
        Basketball newBall = SpawnBall(BallPositions[3].transform.position, BallPositions[3].transform);
        newBall.SetPoint(BallPoints[_currentSpawnIndex]);
        basketballs.Add(newBall);

        if (_currentSpawnIndex < BallPoints.Length - 1)
        {
            _currentSpawnIndex++;
        }
        else
        {
            _currentSpawnIndex = 0;
        }

        ReorderBalls();
        SpawnAnimation(newBall);
    }

    private void SpawnAnimation(Basketball newBall) {
        newBall.transform.localScale = Vector3.zero;
        newBall.transform.DOScale(new Vector3(0.875f, 0.875f, 0.875f), instantiateBallScaleDuration);
    }

    void RemoveAllBalls() {
        foreach (var b in basketballs)
        {
            Destroy(b.gameObject);
        }

        basketballs.Clear();
    }

    void ReorderBalls() {
        for (int i = 0; i < basketballs.Count; i++)
        {
            basketballs[i].transform.SetParent(BallPositions[i].transform);
            basketballs[i].transform.DOLocalMove(Vector3.zero, reorderDuration);
        }
    }
}
