using UnityEngine;
using UnityEngine.UI;

public class slide5 : MonoBehaviour
{
    public Button[] yesButton;     // Correct answer buttons
    public Button[] wrongButton;   // Wrong answer buttons

    public GameObject panel1;      // Current panel
    public GameObject panel2;      // Next panel

    public AudioSource clapSound;  // Assign in Inspector (attach clap sound to this)

    private void Start()
    {
        // Assign listener to each Yes button
        foreach (Button btn in yesButton)
        {
            btn.onClick.AddListener(() => OnCorrectClicked(btn));
        }

        // Assign listener to each Wrong button
        foreach (Button btn in wrongButton)
        {
            btn.onClick.AddListener(OnWrongClicked);
        }
    }

    private void OnCorrectClicked(Button clickedButton)
    {
        SetButtonColor(clickedButton, Color.green);
        Debug.Log("Correct!");

        if (clapSound != null)
            clapSound.Play();

        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    private void OnWrongClicked()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Handheld.Vibrate();
#endif
        Debug.Log("Wrong!");
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = color;
        cb.selectedColor = color;
        button.colors = cb;
    }
}
