using UnityEngine;

public static class AnimationCommand
{
    public static readonly int Idle = Animator.StringToHash("idle2");
    public static readonly int Attack = Animator.StringToHash("attack");
    public static readonly int TripOver = Animator.StringToHash("tripOver");
    public static readonly int Hurt = Animator.StringToHash("hurt");
    public static readonly int Die = Animator.StringToHash("die");
    public static readonly int IsLookUp = Animator.StringToHash("isLookUp");
    public static readonly int IsWalk = Animator.StringToHash("isWalk");
    public static readonly int IsRun = Animator.StringToHash("isRun");
    public static readonly int IsJump = Animator.StringToHash("isJump");
    public static readonly int DubleForward = Animator.StringToHash("DubleForward");
    public static readonly int Fall = Animator.StringToHash("Fall");
    public static readonly int ChangeColor = Animator.StringToHash("ChangeColor");
}
