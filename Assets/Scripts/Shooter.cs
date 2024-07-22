using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] BasketballBar basketballBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetBall() {
        Basketball ball = basketballBar.GetBasketball();
        ball.transform.position = transform.position;
    }
}
