using UnityEngine;
using UnityEngine.UI;

public class SLIDE6 : MonoBehaviour
{
    public GameObject[] animalDots;       // Dots near animals (center row)
    public GameObject[] buttonDots;       // Dots near buttons (left & right sides)
    public string[] correctTags;          // Match by tag: "Tortoise", "Cat", etc.
    public GameObject[] animals;          // Animal GameObjects in same order
    public Material lineMaterial;         // Unlit/Color material for LineRenderer
    public Camera mainCamera;             // Camera used for canvas (if Screen Space - Camera)

    private GameObject[] lineObjects;

    void Start()
    {
        lineObjects = new GameObject[animalDots.Length];

        for (int i = 0; i < animalDots.Length; i++)
        {
            int index = i;
            Button btn = animalDots[index].GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() => {
                    CheckMatchAndDrawLine(index);
                });
            }
        }
    }

    void CheckMatchAndDrawLine(int index)
    {
        GameObject animal = animals[index];
        string expectedTag = correctTags[index];

        if (animal.CompareTag(expectedTag))
        {
            Debug.Log($"✅ Match for {expectedTag}");

            GameObject targetDot = FindMatchingButtonDot(expectedTag);

            if (targetDot != null)
            {
                DrawLine(animalDots[index], targetDot, index, Color.green);
            }
        }
        else
        {
            Debug.Log("❌ Incorrect match");
            DrawLine(animalDots[index], null, index, Color.red);
        }
    }

    GameObject FindMatchingButtonDot(string tag)
    {
        foreach (GameObject dot in buttonDots)
        {
            if (dot.CompareTag(tag))
                return dot;
        }
        return null;
    }

    void DrawLine(GameObject startObj, GameObject endObj, int index, Color color)
    {
        if (lineObjects[index] != null)
        {
            Destroy(lineObjects[index]);
        }

        // Convert UI positions to world positions
        Vector3 worldStart = GetWorldPosition(startObj.GetComponent<RectTransform>());
        Vector3 worldEnd;

        if (endObj != null)
        {
            worldEnd = GetWorldPosition(endObj.GetComponent<RectTransform>());
        }
        else
        {
            worldEnd = worldStart + Vector3.up * 0.5f; // Just a short red line upward if no match
        }

        GameObject lineObj = new GameObject("Line_" + index);
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = 0.025f;
        lr.positionCount = 2;
        lr.useWorldSpace = true;

        lr.SetPosition(0, worldStart);
        lr.SetPosition(1, worldEnd);

        lineObjects[index] = lineObj;
    }

    Vector3 GetWorldPosition(RectTransform uiElement)
    {
        Vector3 worldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            uiElement,
            RectTransformUtility.WorldToScreenPoint(mainCamera, uiElement.position),
            mainCamera,
            out worldPos);
        return worldPos;
    }
}
