using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationChooser : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationName;

    private void Start()
    {
        _animator.SetTrigger(_animationName);
    }
}
