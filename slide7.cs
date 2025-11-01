using UnityEngine;

public class slide7 : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;
    public AudioSource correctSound;  // AudioSource for the correct sound (assign in Inspector)

    public void GoToNextPanel()
    {
        if (currentPanel != null)
        {
            // Play the correct sound before hiding the current panel
            if (correctSound != null)
            {
                correctSound.Play();
            }
            
            currentPanel.SetActive(false);
        }

        if (nextPanel != null)
        {
            nextPanel.SetActive(true);
        }
    }
}
