using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected Animator EnemyAnimator;

    internal virtual void Initilization(Animator animator)
    {
        EnemyAnimator = animator;
    }

    internal abstract void Enable();
    internal abstract void Disable();
}
