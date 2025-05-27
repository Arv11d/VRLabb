using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    
    public string sceneToLoad;

    public void LoadScene()
    {
        
        SceneManager.LoadScene(sceneToLoad);
    }
}
