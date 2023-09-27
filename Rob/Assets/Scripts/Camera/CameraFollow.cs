using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; // Цель
    [SerializeField] private float smoothSpeed = 5f; // Сглаживание движения камеры
    [SerializeField] private Vector3 offset; // Отступ камеры от игрока

    private Player _player;

    private void Start()
    {
        _target = _player.Body;
    }

    [Inject]
    public void Constructor(Player player)
    {
        Debug.Log("ИГрок прокинулся");
        _player = player;
    }

    private void LateUpdate()
    {
        // Проверяем, существует ли цель (игрок)
        if (_player.Body != null)
        {
            // Вычисляем позицию, к которой должна двигаться камера
            Vector3 desiredPosition = _player.Body.position + offset;

            // Интерполируем текущую позицию камеры к желаемой сглаженной позиции
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Устанавливаем позицию камеры равной сглаженной позиции
            transform.position = smoothedPosition;
        }
    }

}
