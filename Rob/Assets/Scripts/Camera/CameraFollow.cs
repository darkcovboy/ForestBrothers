using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cinemachine;

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

    public void Constructor(Player player)
    {
        _player = player;
    }

    private void LateUpdate()
    {
        // Проверяем, существует ли цель (игрок)
        if (_target != null)
        {
            // Вычисляем позицию, к которой должна двигаться камера
            Vector3 desiredPosition = _target.position + offset;

            // Интерполируем текущую позицию камеры к желаемой сглаженной позиции
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Устанавливаем позицию камеры равной сглаженной позиции
            transform.position = smoothedPosition;
        }
    }

}
