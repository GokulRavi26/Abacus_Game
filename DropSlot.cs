using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string acceptedColor;
    public GameObject nextPanel;
    public GameObject currentPanel;
     public AudioSource clapSound;         // Assign in Inspector
    public Animator piggyAnimator;
    private static int correctMatches = 0;
    private static int totalMatches = 4; // Update this based on number of gates
    private bool isMatched = false;

    public void OnDrop(PointerEventData eventData)
    {
        if (DragHandler.itemBeingDragged != null && !isMatched)
        {
            string draggedColor = DragHandler.itemBeingDragged.name.Split('_')[1];

            if (draggedColor.Equals(acceptedColor, System.StringComparison.OrdinalIgnoreCase))
            {
                // Snap the gate to this slot
                DragHandler.itemBeingDragged.transform.SetParent(this.transform);
                DragHandler.itemBeingDragged.transform.position = this.transform.position;

                Debug.Log("Correct match: " + draggedColor);
                isMatched = true;
                correctMatches++;

                // Optional: disable further dragging
                DragHandler.itemBeingDragged.GetComponent<DragHandler>().enabled = false;

                if (correctMatches >= totalMatches)
                {
                    Debug.Log("All gates matched correctly! Moving to next panel.");
                     if (clapSound != null)
                    clapSound.Play();

                if (piggyAnimator != null)
                    piggyAnimator.SetTrigger("Clap");
                    currentPanel.SetActive(false);
                    nextPanel.SetActive(true);
                }
            }
            else
            {
                TriggerWrongVibration();
                Debug.Log("Wrong match: " + draggedColor + " on " + acceptedColor);
            }
        }
    }

    void TriggerWrongVibration()
    {
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
#if UNITY_EDITOR
        Debug.Log("Vibration triggered (mock)");
#endif
    }
}
