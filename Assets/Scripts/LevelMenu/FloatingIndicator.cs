using UnityEngine;

public class FloatingIndicator : MonoBehaviour
{
    public float bobSpeed = 2f;     
    public float bobHeight = 0.01f;  

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.localPosition = startPos + new Vector3(0, offset, 0);
    }
}
