using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class MovementHandler : MonoBehaviour, IMovable
{
    [SerializeField] private float _distanceBetweenPositions;

    public event UnityAction<bool> Moved;

    private float _speed;
    private IInput _movementInput;
    private CharacterController _characterController;
    private Transform _originalPosition;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 direction = _movementInput.GetInputDirection();
        Move(direction);
    }

    public void Init(IInput input, Transform original, float speed)
    {
        _originalPosition = original;
        _movementInput = input;
        _speed = speed;
    }

    public void Move(Vector3 direction)
    {
        _characterController.Move(direction * Time.deltaTime * _speed);

        if (direction != Vector3.zero)
        {
            gameObject.transform.forward = direction;
            Moved?.Invoke(true);
        }
        else
        {
            Moved?.Invoke(false);
        }

        if(Vector3.Distance(transform.position, _originalPosition.position) > _distanceBetweenPositions)
        {
            transform.position = _originalPosition.position;
        }
    }
}
