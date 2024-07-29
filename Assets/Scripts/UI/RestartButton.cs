using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void RestartGame() {
        GameManager.Instance.RestartLevel();
    }
}
