using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class HammerCollisionSound : MonoBehaviour
{
    public AudioClip hitSound;
    public float minVelocity = 0.2f;
    public float volume = 1.0f;

    private AudioSource audioSource;
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = hitSound;

        // Subscribe to new event handlers
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        // Unsubscribe from events
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        rb.isKinematic = false; // or true depending on your desired behavior
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.isKinematic = false; // or false depending on your desired behavior
    }

    void OnCollisionEnter(Collision collision)
    {
        if (rb.linearVelocity.magnitude > minVelocity && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hitSound, volume);
        }
    }
}
