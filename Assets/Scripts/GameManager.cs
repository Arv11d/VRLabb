using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Target Points")]
    public int targetPoints = 10;

    [Header("Object 1 - Move & Rotate")]
    public Transform object1;
    public Vector3 object1MoveOffset;
    public Vector3 object1Rotation;

    [Header("Object 2 - Move & Rotate")]
    public Transform object2;
    public Vector3 object2MoveOffset;
    public Vector3 object2Rotation;

    [Header("Object 3 - Move Only")]
    public Transform object3;
    public Vector3 object3MoveOffset;

    [Header("Movement Settings")]
    public float moveDuration = 2f;

    private int currentPoints = 0;
    private bool hasTriggered = false;

    public void AddPoints(int amount)
    {
        currentPoints += amount;
        Debug.Log($"Points: {currentPoints}");

        if (!hasTriggered && currentPoints >= targetPoints)
        {
            hasTriggered = true;
            TriggerMovement();
        }
    }

    private void TriggerMovement()
    {
        StartCoroutine(MoveAndRotate(object1, object1MoveOffset, object1Rotation));
        StartCoroutine(MoveAndRotate(object2, object2MoveOffset, object2Rotation));
        StartCoroutine(MoveOnly(object3, object3MoveOffset));
    }

    private System.Collections.IEnumerator MoveAndRotate(Transform obj, Vector3 moveOffset, Vector3 rotation)
    {
        Vector3 startPos = obj.position;
        Vector3 endPos = startPos + moveOffset;
        Quaternion startRot = obj.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(rotation);

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            obj.position = Vector3.Lerp(startPos, endPos, t);
            obj.rotation = Quaternion.Slerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.position = endPos;
        obj.rotation = endRot;
    }

    private System.Collections.IEnumerator MoveOnly(Transform obj, Vector3 moveOffset)
    {
        Vector3 startPos = obj.position;
        Vector3 endPos = startPos + moveOffset;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            obj.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.position = endPos;
    }
}
