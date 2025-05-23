using UnityEngine;

public class HammerSound : MonoBehaviour
{
    public AudioClip hitSound;
    public float volume = 1.0f;
    public float minVelocity = 0.2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hitSound != null && rb.linearVelocity.magnitude > minVelocity)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position, volume);
        }
    }
}
