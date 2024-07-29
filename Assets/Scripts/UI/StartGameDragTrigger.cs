using UnityEngine;
using UnityEngine.EventSystems;

public class StartGameDragTrigger : MonoBehaviour, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData) {
        GameManager.Instance.StartGame();
    }
}
