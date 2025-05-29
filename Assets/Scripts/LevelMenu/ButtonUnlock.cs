using UnityEngine;
using UnityEngine.UI;

public class UnlockableButton : MonoBehaviour
{
    public Button buttonToUnlock;
    public GameObject lockObject;

    void Start()
    {
        if (GameSessionData.IsDungeonButtonUnlocked)
        {
            buttonToUnlock.interactable = true;
            if (lockObject != null)
                lockObject.SetActive(false);
        }
    }
}
