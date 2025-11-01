using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // 👈 Add this for scene loading

public class slide : MonoBehaviour
{
    public Button[] optionButtons;
    public int correctNumber = 73;

    public GameObject currentPanel;
    public AudioSource clapSound;
    public AudioSource wrongSound;

    void Start()
    {
        foreach (Button btn in optionButtons)
        {
            btn.onClick.AddListener(() => CheckAnswer(btn));
        }
    }

    void CheckAnswer(Button selectedButton)
    {
        TMP_Text textComponent = selectedButton.GetComponentInChildren<TMP_Text>();
        if (textComponent == null)
        {
            Debug.LogError($"❌ TMP_Text not found in button: {selectedButton.name}");
            return;
        }

        string buttonText = textComponent.text;

        if (int.TryParse(buttonText, out int chosenNumber))
        {
            if (chosenNumber == correctNumber)
            {
                Debug.Log("✅ Correct!");
                if (clapSound) clapSound.Play();

                // Optional: Delay before loading scene to let sound play
                Invoke(nameof(LoadNextScene), 1f);
            }
            else
            {
                Debug.Log("❌ Wrong! Try again.");
#if UNITY_ANDROID && !UNITY_EDITOR
                Handheld.Vibrate();
#endif
                if (wrongSound) wrongSound.Play();
            }
        }
        else
        {
            Debug.LogError("❌ Could not parse button text.");
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("s4"); // Make sure "s2" is added to build settings
    }
}
