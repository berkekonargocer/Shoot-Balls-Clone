using DG.Tweening;
using UnityEngine;

namespace NOJUMPO
{
    public class Money : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player"))
            {
                gameObject.GetComponent<Collider>().enabled = false;
                ScoreManager.Instance.IncrementScore(ScoreManager.Instance.Income);
                transform.DOScale(0, .15f);
                Destroy(gameObject, .2f);
            }
        }
    }
}
