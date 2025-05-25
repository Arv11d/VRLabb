using UnityEngine;
using System.Collections;

public class VoiceLineManager : MonoBehaviour
{
    public static VoiceLineManager Instance { get; private set; }

    private bool isPlaying = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void PlayVoice(AudioSource source, AudioClip clip, System.Action onComplete = null)
    {
        if (!isPlaying)
        {
            Instance.StartCoroutine(Instance.PlayRoutine(source, clip, onComplete));
        }
    }

    private IEnumerator PlayRoutine(AudioSource source, AudioClip clip, System.Action onComplete)
    {
        isPlaying = true;
        source.clip = clip;
        source.Play();
        yield return new WaitForSeconds(clip.length);
        isPlaying = false;
        onComplete?.Invoke();
    }
}
