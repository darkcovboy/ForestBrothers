using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; // ����
    [SerializeField] private float smoothSpeed = 5f; // ����������� �������� ������
    [SerializeField] private Vector3 offset; // ������ ������ �� ������

    private Player _player;

    private void Start()
    {
        _target = _player.Body;
    }

    [Inject]
    public void Constructor(Player player)
    {
        Debug.Log("����� ����������");
        _player = player;
    }

    private void LateUpdate()
    {
        // ���������, ���������� �� ���� (�����)
        if (_player.Body != null)
        {
            // ��������� �������, � ������� ������ ��������� ������
            Vector3 desiredPosition = _player.Body.position + offset;

            // ������������� ������� ������� ������ � �������� ���������� �������
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // ������������� ������� ������ ������ ���������� �������
            transform.position = smoothedPosition;
        }
    }

}
