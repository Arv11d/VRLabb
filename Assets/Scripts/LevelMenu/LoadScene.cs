using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    
    public string sceneToLoad;

    public void LoadScene()
    {
        Debug.Log($"Attempting to load scene: {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }
}
