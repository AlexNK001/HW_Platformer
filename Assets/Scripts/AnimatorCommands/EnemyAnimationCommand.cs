using UnityEngine;

public static class EnemyAnimationCommand
{
    public static readonly int Death = Animator.StringToHash("death");
    public static readonly int Evade = Animator.StringToHash("evade_1");
    public static readonly int FirstHit = Animator.StringToHash("hit_1");
    public static readonly int SecondHit = Animator.StringToHash("hit_2");
    public static readonly int FirstIdle = Animator.StringToHash("idle_1");
    public static readonly int SecondIdle = Animator.StringToHash("idle_2");
    public static readonly int Run = Animator.StringToHash("run");
    public static readonly int FirstSkill = Animator.StringToHash("skill_1");
    public static readonly int SecondSkil = Animator.StringToHash("skill_2");
    public static readonly int Walk = Animator.StringToHash("walk");
}
