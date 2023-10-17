using UnityEngine;

[RequireComponent(typeof(MovementHandler))]
public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private Animal _animal; 
    [SerializeField] private Animator _animator;
    [SerializeField] private MovementHandler _movementHandler;

    private readonly string _diyngAnimationKey = "IsDiyng";
    private readonly string _moveAnimationKey = "Speed";
    private readonly string _winAnimationKey = "IsWinning";

    private void OnEnable()
    {
        _movementHandler.Moved += MoveAnimation;
        _animal.OnDiyng += DyingAnimation;
        _animal.OnCelebration += CelebrationAnimation;
    }

    private void OnDisable()
    {
        _movementHandler.Moved -= MoveAnimation;
        _animal.OnDiyng -= DyingAnimation;
        _animal.OnCelebration -= CelebrationAnimation;
    }

    private void MoveAnimation(float speed)
    {
        _animator.SetFloat(_moveAnimationKey, speed);
    }

    private void CelebrationAnimation()
    {
        _animator.SetBool(_winAnimationKey, true);
    }

    private void DyingAnimation()
    {
        _animator.SetBool(_diyngAnimationKey, true);
    }
}
