using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerLoader : MonoBehaviour
{
    public string sceneToLoad = "Dungeon"; 
    

    private bool hasLoaded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameSessionData.IsDungeonButtonUnlocked = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
