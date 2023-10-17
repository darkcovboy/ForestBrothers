using DG.Tweening;
using UnityEngine;

public class AnimationObjectMovement : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform _objectToMove; // Поле для указания объекта, который нужно двигать
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _mveDuration = 2f;

    private void Start()
    {
        var prefab = Instantiate(_objectPrefab, _startPoint.position, Quaternion.identity);
        _objectToMove = prefab.transform;
        MoveObject();
    }

    private void MoveObject()
    {
        _objectToMove.DOMove(_endPoint.position, _mveDuration).SetEase(Ease.Linear).OnComplete(() => {
            _objectToMove.position = _startPoint.position;
            MoveObject();
        });
    }

}
