using UnityEngine;
using UnityEngine.UI;

public class LongestImageGame : MonoBehaviour
{
    public Button[] imageButtons;  // Assign 5 buttons in inspector
    public GameObject panel1;      // Current panel with images
    public GameObject panel2;      // Next panel to show

    public AudioSource clapSound;  // Assign in Inspector (AudioSource with clap sound)

    private int correctIndex = -1;

    void Start()
    {
        if (imageButtons.Length < 5)
        {
            Debug.LogError("Please assign 5 image buttons.");
            return;
        }

        float maxLength = 0;
        for (int i = 0; i < imageButtons.Length; i++)
        {
            Image img = imageButtons[i].GetComponent<Image>();
            Sprite sprite = img.sprite;
            float length = Mathf.Max(sprite.rect.width, sprite.rect.height);

            if (length > maxLength)
            {
                maxLength = length;
                correctIndex = i;
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
        if (index == correctIndex)
        {
            Debug.Log("Correct image clicked!");

            // Play clap sound
            if (clapSound != null)
                clapSound.Play();

            panel1.SetActive(false);
            panel2.SetActive(true);
        }
        else
        {
            Debug.Log("Wrong image. Try again.");

            // Mobile vibration
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }
}
