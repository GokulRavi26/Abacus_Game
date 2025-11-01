using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel;
    [SerializeField] private GameObject previousPanel;

    public void OnBackButtonPressed()
    {
        if (currentPanel != null && previousPanel != null)
        {
            currentPanel.SetActive(false);
            previousPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Please assign both current and previous panels in the inspector.");
        }
    }
}