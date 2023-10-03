using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour, ITouchable
{
    [SerializeField] private ItemData _itemData;

    private Rigidbody _rigibody;
    private Vector3 _defaultRotation;
    private Collider _collider;

    public int Reward => _itemData.Reward;

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _defaultRotation = transform.eulerAngles;
    }

    public void ConnectTo(Transform position)
    {
        OffPhysics();
        StartCoroutine(MoveToTarget(position));
    }

    public void ConnecTo(ItemCollector itemCollector)
    {
        OffPhysics();
        transform.DOMove(itemCollector.Point.position, _itemData.MoveDuration).SetEase(Ease.InOutSine).OnComplete(DestroyObject);
    }

    [Button]
    public void Disconnect()
    {
        transform.SetParent(null);
        OnPhysics();
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        transform.DORotate(_defaultRotation, _itemData.RotateDuration);

        while(Vector3.Distance(transform.position, target.position) > _itemData.DistanceBetweenTargets)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * _itemData.SpeedMoveToTarget);
            yield return null;
        }

        transform.SetParent(target);
    }

    private void OffPhysics()
    {
        _collider.enabled = false;
        _rigibody.isKinematic = true;
    }

    private void OnPhysics()
    {
        _collider.enabled = true;
        _rigibody.isKinematic = false;
    }

    private void DestroyObject() => DestroyImmediate(gameObject);
}
