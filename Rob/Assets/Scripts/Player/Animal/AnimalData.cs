using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName ="Animal Data", menuName = "Animal Data")]
public class AnimalData : ScriptableObject
{
    [SerializeField]
    private AnimalType _animalType;
    [ShowInInspector]
    public Animal AnimalPrefab { get; private set; }

    [SerializeField]
    private ParticleSystem _appereanceEffects;
    [SerializeField,Range(1f, 10)]
    private float _speedMove;
    public AnimalType Animal => _animalType;
    public float Speed => _speedMove;
    public ParticleSystem AppereanceEffect => _appereanceEffects;
}

public enum AnimalType
{
    Racoon,
    CameraMan
}
