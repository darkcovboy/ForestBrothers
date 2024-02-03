using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Animal Data", menuName = "Animal Data")]
public class AnimalData : ScriptableObject
{
    [Header("MainSettings")]
    [SerializeField] private AnimalType _animalType;
    [SerializeField] private Animal _animalPrefab;
    [SerializeField,Range(1, 10)] private float _speedMove;
    public AnimalType AnimalType => _animalType;
    public float Speed => _speedMove;
    public Animal AnimalPrefab => _animalPrefab;
}

public enum AnimalType
{
    Racoon,
    Owl,
    Capybara
}
