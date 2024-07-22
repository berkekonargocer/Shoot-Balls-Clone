using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballBar : MonoBehaviour
{
    [SerializeField] GameObject basketballPrefab;
    [SerializeField] Transform[] ballPositions;

    [SerializeField] float reorderSpeed;

    [SerializeField] List<Basketball> basketballs = new List<Basketball>();

    void Start() {

    }

    void Update() {

    }

    public Basketball GetBasketball() {
        Basketball ball = basketballs[0];
        basketballs.RemoveAt(0);
        CreateNewBall();

        return ball;
    }

    void CreateNewBall() {
        Basketball newBall = Instantiate(basketballPrefab, ballPositions[3].position, Quaternion.identity, transform).GetComponent<Basketball>();
        basketballs.Add(newBall);
        ReorderBalls();
    }

    void ReorderBalls() {
        for (int i = 0; i < basketballs.Count; i++)
        {
            basketballs[i].transform.position = ballPositions[i].position;
        }
    }
}
