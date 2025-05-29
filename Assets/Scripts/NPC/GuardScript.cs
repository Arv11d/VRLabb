using UnityEngine;

public class GuardScript : MonoBehaviour
{
    public Transform shoulderL;
    public Transform elbowL;
    public Transform handL;

    public Vector3 shoulderTargetRot;
    public Vector3 elbowTargetRot;
    public Vector3 handTargetRot;

    public Vector3 shoulderIdleRot;
    public Vector3 elbowIdleRot;
    public Vector3 handIdleRot;

    public float speed = 2f;
    private bool block = false;

    void Update()
    {
        Quaternion shoulderTarget = Quaternion.Euler(block ? shoulderTargetRot : shoulderIdleRot);
        Quaternion elbowTarget = Quaternion.Euler(block ? elbowTargetRot : elbowIdleRot);
        Quaternion handTarget = Quaternion.Euler(block ? handTargetRot : handIdleRot);

        shoulderL.localRotation = Quaternion.Slerp(shoulderL.localRotation, shoulderTarget, Time.deltaTime * speed);
        elbowL.localRotation = Quaternion.Slerp(elbowL.localRotation, elbowTarget, Time.deltaTime * speed);
        handL.localRotation = Quaternion.Slerp(handL.localRotation, handTarget, Time.deltaTime * speed);
    }

    public void TriggerBlock() => block = true;
    public void ResetPose() => block = false;

}
