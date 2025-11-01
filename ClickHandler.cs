using UnityEngine;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    private LineConnector connector;

    void Start()
    {
        connector = FindObjectOfType<LineConnector>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        connector.OnPointClicked(this.gameObject);
    }
}
