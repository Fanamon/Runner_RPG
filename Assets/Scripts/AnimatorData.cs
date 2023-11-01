using UnityEngine;

public static class AnimatorData
{
    public static class PlayerAnimatorParameters
    {
        public static readonly int Run = Animator.StringToHash(nameof(Run));
        public static readonly int Die = Animator.StringToHash(nameof(Die));
    }

    public static class ObstacleParameters
    {
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int Die = Animator.StringToHash(nameof(Die));
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));
    }
}
