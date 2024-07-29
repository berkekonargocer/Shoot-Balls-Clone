using UnityEngine;
using UnityEngine.EventSystems;

public class StartGameTrigger : MonoBehaviour, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData) {
        GameManager.Instance.StartGame();
    }
}
