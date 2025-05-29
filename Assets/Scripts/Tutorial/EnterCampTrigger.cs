using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerLoader : MonoBehaviour
{
    public string sceneToLoad = "Dungeon"; 
    

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameSessionData.IsDungeonButtonUnlocked = true;
            GameSessionData.HasPlayedDungeonVoiceLine = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
