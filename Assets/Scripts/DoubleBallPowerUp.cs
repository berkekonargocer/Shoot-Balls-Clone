using DG.Tweening;
using UnityEngine;

public class DoubleBallPowerUp : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<Shooter>().StartDoubleBallPowerUp(6f);
            transform.gameObject.GetComponent<Collider>().enabled = false;
            transform.DOScale(0, 0.15f);
            Destroy(gameObject, 0.2f);
        }
    }
}