using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using Sirenix.OdinInspector;
using System.Linq;

public class Player : MonoBehaviour
{
    private const int _mainCharacterIndex = 0;

    [SerializeField] private AnimalData _animalData;
    [SerializeField] private Transform _positions;
    [SerializeField] private int _startCapacity;
    [SerializeField] private Transform _parent;

    public Transform Body => _characters[_mainCharacterIndex].transform;

    private List<Animal> _characters = new List<Animal>();
    private IInput _inputType;
    private List<Transform> _charactersPositions = new List<Transform>();

    private int _maxCapacity = 10;
    private void Start()
    {
        if (_startCapacity > _positions.childCount)
            throw new NullReferenceException();

        SetPositions();
        Create();
    }

    [Inject]
    public void Constructor(IInput input)
    {
        _inputType = input;
    }

    private void SetPositions()
    {
        Transform[] positions = _positions.gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform position in positions)
        {
            if(position != _positions)
            {
                _charactersPositions.Add(position);
            }
        }
    }
    public void RemoveCharacter(Animal animal)
    {
        _characters.Remove(animal);
        Destroy(animal.gameObject);
        foreach (var item in _characters)
        {
            Debug.Log(item.gameObject.transform.position);
        }
    }

    private void Create()
    {
        for (int i = 0; i < _startCapacity; i++)
        {
            AddCharacter(i);
        }

        _positions.SetParent(_characters[0].transform, false);
    }

    private void AddCharacter(int index)
    {
        if(index > _characters.Count)
        {
            return;
        }
        else
        {
            Animal character = Instantiate(_animalData.AnimalPrefab, _charactersPositions[index].position, Quaternion.identity, _parent);
            character.Init(this);
            character.GetComponent<MovementHandler>().Init(_inputType, _charactersPositions[index], _animalData.Speed);
            _characters.Add(character);
        }
    }

    [Button]
    private void AddCharacter()
    {
        if(_characters.Count < _maxCapacity)
        {
            int indexPoint = _characters.Count;
            Animal character = Instantiate(_animalData.AnimalPrefab, _charactersPositions[indexPoint].position, Quaternion.identity, _parent);
            character.Init(this);
            character.GetComponent<MovementHandler>().Init(_inputType, _charactersPositions[indexPoint], _animalData.Speed);
            _characters.Add(character);
        }
    }

    private void RemoveDiyngAnimals()
    {

    }
}
