using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class MovementHandler : MonoBehaviour, IMovable
{
    [SerializeField] private float _distanceBetweenPositions;

    public event UnityAction<float> Moved;

    private float _speed;
    private IInput _movementInput;
    private CharacterController _characterController;
    private Transform _originalPosition;
    private float _defaultY;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _defaultY = transform.position.y;
    }

    private void Update()
    {
        Vector3 direction = _movementInput.GetInputDirection();
        Move(direction);
        //transform.position = new Vector3(transform.position.x, _defaultY, transform.position.z);
    }

    public void Init(IInput input, Transform original, float speed)
    {
        _originalPosition = original;
        _movementInput = input;
        _speed = speed;
    }
    public void Move(Vector3 direction)
    {
        Vector3 moveDirection = direction * _speed;
        moveDirection.y = _defaultY;
        _characterController.Move(moveDirection * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            gameObject.transform.forward = direction;
        }

        Moved?.Invoke(_characterController.velocity.magnitude);

        if (Vector3.Distance(transform.position, _originalPosition.position) > _distanceBetweenPositions)
        {
            transform.position = _originalPosition.position;
        }
    }
}
