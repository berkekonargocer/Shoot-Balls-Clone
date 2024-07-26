using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseGame();
        }
    }
}
