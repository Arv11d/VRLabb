using UnityEngine;
using UnityEngine.UI;
using BNG;

public class UnlockableButton2 : MonoBehaviour
{
    public BNG.Button ButtonToActivate
        ;
    public GameObject lockObject;
    public GameObject uiTextObject;

    void Start()
    {
        if (GameSessionData.IsDungeonButtonUnlocked2)
        {
            if (ButtonToActivate != null)
                ButtonToActivate.enabled = true;

            if (lockObject != null)
                lockObject.SetActive(false);

            if (uiTextObject != null)
                uiTextObject.SetActive(true);
        }

    }
}
