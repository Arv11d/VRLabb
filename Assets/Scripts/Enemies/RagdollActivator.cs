using UnityEngine;
using UnityEngine.AI;

public class RagdollActivator : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;
    
    [Header("Animation & Audio")]
    public Animator animator;
    public AudioClip hitSound;
    public AudioClip deathSound;
    
    [Header("Components")]
    private Rigidbody[] allRigidbodies;
    private Collider[] allColliders;
    private NavMeshAgent agent;
    private bool isDead = false;

    [Header("Scripts to Disable")]
    public MonoBehaviour[] scriptsToDisable;

    [Header("Game Management")]

    public GameManager gameManager;
    public int pointsToAdd = 1;

    void Awake()
    {
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
        allColliders = GetComponentsInChildren<Collider>();
        agent = GetComponent<NavMeshAgent>();
        
        currentHealth = maxHealth;
        SetRagdoll(false);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) 
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0f, currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            
            if (animator != null && animator.enabled)
                animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        if (isDead) return;
        
        isDead = true;
        
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        
        if (gameManager != null)
            gameManager.AddPoints(pointsToAdd);
        
        SetRagdoll(true);
    }

    public void SetRagdoll(bool state)
    {
        if (animator != null)
            animator.enabled = !state;

        foreach (var rb in allRigidbodies)
            rb.isKinematic = !state;

        foreach (var col in allColliders)
        {
            if (col.gameObject != this.gameObject)
                col.enabled = state;
        }

        if (agent != null)
            agent.enabled = !state;

        if (obstacle != null)
            obstacle.enabled = state; 

        if (scriptsToDisable != null)
        {
            foreach (var script in scriptsToDisable)
                script.enabled = !state;
        }

    }
    
    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
    
    public bool IsDead()
    {
        return isDead;
    }
    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}