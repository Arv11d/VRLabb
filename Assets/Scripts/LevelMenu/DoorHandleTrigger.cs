using BNG;
using UnityEngine;


public class DoorHandleTrigger : GrabbableEvents
{

    public GameObject levelMenu;
    private GameObject levelMenuInstance;
    public GameObject grabIndicator;

    public Transform spawnPoint;
    public GameObject rightHandPointer;

    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);

        if (levelMenuInstance != null) return;

        if (grabIndicator != null)
            grabIndicator.SetActive(false);

        levelMenuInstance = Instantiate(levelMenu);
        levelMenuInstance.transform.position = spawnPoint.position;
        levelMenuInstance.transform.rotation = spawnPoint.rotation;

        if (rightHandPointer != null)
        {
            rightHandPointer.SetActive(true);
        }
    }
}

