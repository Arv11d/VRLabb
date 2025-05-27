using UnityEngine;

public class VelocityTracker : MonoBehaviour
{
    public Vector3 CurrentVelocity { get; private set; }
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        CurrentVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
        lastPosition = transform.position;
    }
}
