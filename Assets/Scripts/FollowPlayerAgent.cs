using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Camera.main.transform; // Headset position
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 target = player.position;
            target.y = transform.position.y; // stay level
            agent.SetDestination(target);
        }
    }
}
