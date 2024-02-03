using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementHandler))]
public class AnimationSwitcher : MonoBehaviour
{
    private Animator _animator;
    private MovementHandler _movementHandler;

    private readonly string _moveAnimationKey = "Speed";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movementHandler = GetComponent<MovementHandler>();
        _movementHandler.Moved += MoveAnimation;
    }

    private void OnDisable()
    {
        _movementHandler.Moved -= MoveAnimation;
    }

    private void MoveAnimation(float speed)
    {
        _animator.SetFloat(_moveAnimationKey, speed);
    }
}
