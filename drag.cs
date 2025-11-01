using UnityEngine;
using UnityEngine.EventSystems;

public class Dragg : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetCanvasScaleFactor();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Optional: return to original position if dropped incorrectly
        if (!eventData.pointerEnter || !eventData.pointerEnter.CompareTag("DropSlot"))
            transform.position = originalPosition;
    }

    private float GetCanvasScaleFactor()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        return canvas ? canvas.scaleFactor : 1f;
    }
}
