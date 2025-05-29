using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToDungeon : MonoBehaviour
{
    public string sceneToLoad = "Dungeon";




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameSessionData.IsDungeonButtonUnlocked2 = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
