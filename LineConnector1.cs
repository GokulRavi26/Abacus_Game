using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineConnector : MonoBehaviour
{
    [Header("References")]
    public GameObject linePrefab;
    public AudioSource clapSound;

    [Tooltip("Panel that contains the current matching UI")]
    public GameObject panel1;

    [Tooltip("Panel that should appear after all matches are correct")]
    public GameObject panel2;

    [Header("Matching Settings")]
    [Tooltip("Total number of correct matches needed to switch panels")]
    public int totalMatchesNeeded = 0;

    private ConnectPoint startPoint = null;
    private int correctMatches = 0;

    public void OnPointClicked(GameObject clickedObject)
    {
        Debug.Log("Clicked: " + clickedObject.name);

        ConnectPoint cp = clickedObject.GetComponent<ConnectPoint>();
        if (cp == null)
        {
            Debug.LogWarning("ConnectPoint missing on clicked object!");
            return;
        }

        if (startPoint == null)
        {
            startPoint = cp;
            Debug.Log("Start Point Set: " + cp.pointID);
        }
        else
        {
            Debug.Log("End Point Clicked: " + cp.pointID);
            if (startPoint != cp)
            {
                DrawLine(startPoint.gameObject, cp.gameObject);
            }
            startPoint = null;
        }
    }

    void DrawLine(GameObject from, GameObject to)
    {
        GameObject newLine = Instantiate(linePrefab, transform);
        LineRenderer lr = newLine.GetComponent<LineRenderer>();
        lr.positionCount = 2;

        Vector3 fromPos = from.transform.position;
        Vector3 toPos = to.transform.position;
        fromPos.z = toPos.z = Camera.main.nearClipPlane + 1f;

        lr.SetPosition(0, fromPos);
        lr.SetPosition(1, toPos);

        string fromID = from.GetComponent<ConnectPoint>().pointID;
        string toID = to.GetComponent<ConnectPoint>().pointID;

        if (fromID == toID)
        {
            lr.startColor = lr.endColor = Color.green;

            if (from.TryGetComponent(out Button fromBtn)) fromBtn.interactable = false;
            if (to.TryGetComponent(out Button toBtn)) toBtn.interactable = false;

            clapSound?.Play();

            correctMatches++;
            Debug.Log("Correct Matches: " + correctMatches);

            if (correctMatches >= totalMatchesNeeded)
            {
                StartCoroutine(SwitchPanelsWithDelay());
            }
        }
        else
        {
            lr.startColor = lr.endColor = Color.red;

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
            Destroy(newLine, 1f);
        }
    }

    IEnumerator SwitchPanelsWithDelay()
    {
        yield return new WaitForSeconds(1f);

        if (panel1 != null) panel1.SetActive(false);
        if (panel2 != null) panel2.SetActive(true);
        else Debug.LogWarning("Panel2 not set in inspector.");
    }
}
