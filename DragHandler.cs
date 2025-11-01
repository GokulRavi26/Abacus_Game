using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform originalParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(transform.root); // bring to front
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == originalParent || transform.parent == transform.root)
        {
            transform.SetParent(originalParent);
            transform.position = startPosition;
        }
    }
}
