using UnityEngine;
using UnityEngine.UI;

public class UnlockableButton2 : MonoBehaviour
{
    public Button buttonToUnlock;
    public GameObject lockObject;

    void Start()
    {
        if (GameSessionData.IsDungeonButtonUnlocked2)
        {
            buttonToUnlock.interactable = true;
            if (lockObject != null)
                lockObject.SetActive(false);
        }
    }
}
