using UnityEngine;

public class SpeedTriggerChildRelay : MonoBehaviour
{
    public SpeedTrigger speedTrigger; // Reference to the parent script

    private void OnTriggerEnter(Collider other)
    {
        if (speedTrigger != null && other.CompareTag("Player"))
        {
            speedTrigger.OnChildTriggerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (speedTrigger != null && other.CompareTag("Player"))
        {
            speedTrigger.OnChildTriggerExit(other);
        }
    }
}
