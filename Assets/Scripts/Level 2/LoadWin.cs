using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWin : MonoBehaviour
{
    public string sceneToLoad = "VictoryScene";




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameSessionData.IsDungeonButtonUnlocked2 = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
