using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public string expectedValue; // Set in Inspector
    public GameObject nextPanel;
    public GameObject currentPanel;
    public int totalCorrectMatches;

    public AudioSource clapSound;         // Assign in Inspector
 //   public Animator piggyAnimator;        // Assign in Inspector (has "Clap" trigger)

    private static int correctMatches = 0;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            string droppedValue = dropped.name.Replace("Draggable", "");

            if (droppedValue == expectedValue)
            {
                dropped.transform.position = transform.position;
                Debug.Log("Correct!");

                // Play sound and animation
                if (clapSound != null)
                    clapSound.Play();

               // if (piggyAnimator != null)
               //     piggyAnimator.SetTrigger("Clap");

                correctMatches++;

                if (correctMatches >= totalCorrectMatches)
                {
                    Debug.Log("All matches correct! Switching panels.");
                    currentPanel.SetActive(false);
                    nextPanel.SetActive(true);
                    correctMatches = 0;
                }
            }
            else
            {
                Debug.Log("Incorrect!");
#if UNITY_ANDROID || UNITY_IOS
                Handheld.Vibrate(); // Vibration only on mobile
#endif
            }
        }
    }
}
