using UnityEngine;

public class DoubleBallPowerUp : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<Shooter>().StartDoubleBallPowerUp(6f);
        }
    }
}