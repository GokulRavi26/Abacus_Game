using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Load Scene2 *additively* into Scene1
        SceneManager.LoadScene("s2", LoadSceneMode.Additive);
    }
}
