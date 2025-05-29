using UnityEngine;

public class SpeedTrigger : MonoBehaviour
{
    public Animator animator;
    public string parameterName = "Speed";

    // These methods get called by the child
    public void OnChildTriggerEnter(Collider other)
    {
        Debug.Log("Inside Playertrigger (from child)");
        animator.SetFloat(parameterName, 0f);
    }

    public void OnChildTriggerExit(Collider other)
    {
        animator.SetFloat(parameterName, 2f); // Reset or default value
    }
}
