using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public ThrowingLogic throwingLogic;  // Assign the Throwing child script in Inspector

    // Called from animation event on parent
    public void TriggerThrowing()
    {
        if (throwingLogic != null)
        {
            throwingLogic.DetachAndThrow();
        }
        else
        {
            Debug.LogWarning("ThrowingLogic not assigned in EnemyController!");
        }
    }
}
