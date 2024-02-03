using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New ItemData", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int _reward;
    public int Reward => _reward;
    [SerializeField] private float _distanceBetweenTargets;
    public float DistanceBetweenTargets => _distanceBetweenTargets;

    [SerializeField] private float _speedMoveToTarget;
    public float SpeedMoveToTarget => _speedMoveToTarget;
    [SerializeField] private float _moveDuration;
    public float MoveDuration => _moveDuration;
    [SerializeField] private float _rotateDuration;
    public float RotateDuration => _rotateDuration;

}
