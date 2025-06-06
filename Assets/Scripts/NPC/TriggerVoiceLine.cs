using UnityEngine;

public class TriggerVoiceLine : MonoBehaviour
{
    public AudioClip voiceLine;
    public GameObject npc;
    public string Tag;
    public bool ReTriggable;
    public bool useDungeonFlag = false;
    public bool useReturnFlag = false;

    private bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered || VoiceLineManager.Instance.IsPlaying()) return;

        if ((useDungeonFlag && GameSessionData.HasPlayedDungeonVoiceLine) ||
            (useReturnFlag && GameSessionData.HasPlayedReturnVoiceLine))
            return;

        if (other.CompareTag(Tag))
        {
            AudioSource npcAudio = npc.GetComponent<AudioSource>();

            if (npcAudio != null && voiceLine != null)
            {
                VoiceLineManager.Instance.PlayVoice(npcAudio, voiceLine, () =>
                {
                    if (!ReTriggable)
                        hasBeenTriggered = true;

                    if (useDungeonFlag)
                        GameSessionData.HasPlayedDungeonVoiceLine = true;

                    if (useReturnFlag)
                        GameSessionData.HasPlayedReturnVoiceLine = true;
                });
            }
        }
    }
}
