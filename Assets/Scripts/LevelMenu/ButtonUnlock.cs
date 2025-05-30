using UnityEngine;
using UnityEngine.UI;
using BNG;

public class UnlockableButton : MonoBehaviour
{
    public BNG.Button ButtonToActivate
        ;
    public GameObject lockObject;
    public GameObject uiTextObject;

    void Start()
    {
        if (GameSessionData.IsDungeonButtonUnlocked)
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
