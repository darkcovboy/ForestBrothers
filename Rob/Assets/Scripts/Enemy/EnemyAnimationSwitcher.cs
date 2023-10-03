using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSwitcher : MonoBehaviour
{
    private const string AttackAnimationKey = "Catch";
    private const string DancingAnimationKey = "IsDancing";

    [SerializeField] private Animator _animator;

    public void PlayAttackAnimation()
    {
        _animator.Play(AttackAnimationKey);
    }

    public void PlayDanceAnimation()
    {
        _animator.SetBool(DancingAnimationKey, true);
    }
}
