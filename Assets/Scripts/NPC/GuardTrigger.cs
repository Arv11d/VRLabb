using UnityEngine;

public class GuardTrigger : MonoBehaviour
{
    public AudioSource kingVoiceLine;
    private bool voicePlayed = false;
    public GuardScript leftGuard;
    public GuardScript rightGuard;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (leftGuard != null) leftGuard.TriggerBlock();
            if (rightGuard != null) rightGuard.TriggerBlock();

            if (!voicePlayed && kingVoiceLine != null)
            {
                Debug.Log("PLAYING VOICELINE");
                kingVoiceLine.Play();
                voicePlayed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (leftGuard != null) leftGuard.ResetPose();
            if (rightGuard != null) rightGuard.ResetPose();
        }
    }
}
