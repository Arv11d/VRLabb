using UnityEngine;

public class HammerHitDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var ragdoll = collision.collider.GetComponentInParent<RagdollActivator>();
        if (ragdoll != null)
        {
            ragdoll.SetRagdoll(true);

            // Get the root GameObject with the FollowPlayerAgent
            FollowPlayerAgent ai = ragdoll.transform.root.GetComponent<FollowPlayerAgent>();
            if (ai != null)
            {
                ai.DisableMovement();
            }
            else
            {
                Debug.LogWarning("FollowPlayerAgent not found on root of " + ragdoll.transform.root.name);
            }
        }
    }

}
