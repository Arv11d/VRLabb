using UnityEngine;

public class BlockedAttack : MonoBehaviour
{
    public Animator animator;
    public AudioSource WeaponAudioSource;
    public AudioClip woodBlock;
    public AudioClip SteelBlock;

    void OnTriggerEnter(Collider other)
    {
        CancelAttack();
        if (other.CompareTag("Weapon"))
            WeaponAudioSource.PlayOneShot(SteelBlock);
        if (other.CompareTag("WoodShield"))
            WeaponAudioSource.PlayOneShot(woodBlock);
    }
    void CancelAttack()
    {
        if (animator != null)
        {
            Debug.Log("Interrupt!");
            animator.SetTrigger("Interrupt"); // Use trigger to break the attack
        }

    }

}
