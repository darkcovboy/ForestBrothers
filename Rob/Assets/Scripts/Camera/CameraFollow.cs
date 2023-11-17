using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private Vector3 _offsetMobile;
    [SerializeField] private Vector3 _offsetDesktop;
    [SerializeField] private Vector3 _winOffset;

    private Player _player;
    private Vector3 _offset;
    private IGameResultHandler _gameResultHandler;

    [Inject]
    public void Constructor(Player player, IGameResultHandler gameResultHandler)
    {
        _player = player;
        _gameResultHandler = gameResultHandler;
        _gameResultHandler.OnGameWinning += OnWin;

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _offset = _offsetMobile;
        }
        else
        {
            _offset = _offsetDesktop;
        }
    }

    private void LateUpdate()
    {
        if (_player.Body != null)
        {
            Vector3 desiredPosition = _player.Body.position + _offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }

    private void OnWin()
    {
        _offset = _winOffset;
    }
}
