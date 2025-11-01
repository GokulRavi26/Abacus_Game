using UnityEngine;
using UnityEngine.UI;

public class SmallestNumberGame : MonoBehaviour
{
    public Button[] imageButtons;    // Buttons with number images
    public int[] numberValues;       // Directly assigned numbers in Inspector
    public GameObject panel1;        // Current panel
    public GameObject panel2;        // Next panel
    public AudioSource clapSound;    // Assign a clap AudioSource in Inspector

    private int smallestIndex = -1;

    void Start()
    {
        if (imageButtons.Length != numberValues.Length)
        {
            Debug.LogError("Buttons and Number Values must be equal in count!");
            return;
        }

        int smallestValue = int.MaxValue;
        for (int i = 0; i < numberValues.Length; i++)
        {
            if (numberValues[i] < smallestValue)
            {
                smallestValue = numberValues[i];
                smallestIndex = i;
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
        if (index == smallestIndex)
        {
            Debug.Log("Correct! Smallest number clicked.");

            if (clapSound != null)
                clapSound.Play();

            panel1.SetActive(false);
            panel2.SetActive(true);
        }
        else
        {
            Debug.Log("Wrong image. Try again!");

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }
}
