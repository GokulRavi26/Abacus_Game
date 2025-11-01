using UnityEngine;
using UnityEngine.UI;

public class ClosestImageToDoor : MonoBehaviour
{
    public Button[] imageButtons;        // Assign 5 UI Buttons
    public Transform[] imagePositions;   // Assign corresponding positions of the images
    public Transform doorPosition;       // Assign the door's position
    public GameObject panel1;
    public GameObject panel2;

    public AudioSource clapSound;        // Drag an AudioSource with the clap sound in Inspector

    private int closestIndex = -1;

    void Start()
    {
        if (imageButtons.Length != imagePositions.Length || imageButtons.Length == 0)
        {
            Debug.LogError("Make sure imageButtons and imagePositions have the same count and are not empty.");
            return;
        }

        float minDistance = float.MaxValue;
        for (int i = 0; i < imagePositions.Length; i++)
        {
            float dist = Vector3.Distance(imagePositions[i].position, doorPosition.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestIndex = i;
            }
        }

        for (int i = 0; i < imageButtons.Length; i++)
        {
            int index = i;
            imageButtons[i].onClick.AddListener(() => OnImageClicked(index));
        }
    }

    void OnImageClicked(int index)
    {
        if (index == closestIndex)
        {
            Debug.Log("Correct image (closest to door) clicked!");

            if (clapSound != null)
                clapSound.Play();

            panel1.SetActive(false);
            panel2.SetActive(true);
        }
        else
        {
            Debug.Log("Incorrect image. Try again.");

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }
}
