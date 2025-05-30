using UnityEngine;

public class GuardTrigger : MonoBehaviour
{
    public AudioSource kingVoiceLine;
    private bool voicePlayed = false;
    public GuardScript leftGuard;
    public GuardScript backleftGuard;
    public GuardScript backrightGuard;
    public GuardScript rightGuard;
    public BoxCollider enableExitTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enableExitTrigger != null)
                enableExitTrigger.isTrigger = true;
            if (leftGuard != null) leftGuard.TriggerBlock();
            if (rightGuard != null) rightGuard.TriggerBlock();
            if (backrightGuard != null) backrightGuard.TriggerBlock();
            if (backleftGuard != null) backleftGuard.TriggerBlock();

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
            if (backrightGuard != null) backrightGuard.ResetPose();
            if (backleftGuard != null) backleftGuard.ResetPose();
        }
    }
}
