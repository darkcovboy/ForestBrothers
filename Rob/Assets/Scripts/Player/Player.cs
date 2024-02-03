using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
    private const int _mainCharacterIndex = 0;

    [SerializeField] private Transform _positions;
    [SerializeField] private int _capacity;
    [SerializeField] private Transform _parent;

    public Transform Body => _positions;
    public int Capacity => _capacity;

    public int MaxCapacity => _maxCapacity;

    private List<Animal> _characters = new List<Animal>();
    private List<Transform> _charactersPositions = new List<Transform>();

    private IInput _inputType;
    private PlayerSave _playerSave;
    private AnimalData _animalData;

    private int _maxCapacity;

    private void Start()
    {
        if (_capacity > _positions.childCount)
            throw new NullReferenceException();

        SetPositions();
        Create();
    }

    private void Update()
    {
        if (_characters[0] != null)
        {
            _positions.transform.position = _characters[0].transform.position;
        }
    }

    private void OnDisable()
    {
        _playerSave.OnSkinChanged -= ChangeSkin;
    }

    public void Initialize(int capacity, AnimalData animalData)
    {
        _capacity = capacity;
        _animalData = animalData;
    }

    [Inject]
    public void Constructor(IInput input, PlayerSave playerSave)
    {
        _inputType = input;
        _playerSave = playerSave;
        _playerSave.OnSkinChanged += ChangeSkin;
    }

    public void RemoveCharacter(Animal animal)
    {
        _characters.Remove(animal);
        Destroy(animal.gameObject);
    }

    public void RemoveCharacter(int i)
    {
        Animal animal = _characters[i];
        _characters.RemoveAt(i);
        Destroy(animal.gameObject);
    }

    public void ChangeSkin(AnimalData animalData)
    {
        _animalData = animalData;

        while(_characters.Count > 0)
        {
            int index = 0;
            RemoveCharacter(index);
        }

        Create();
    }

    public void AddCharacter()
    {
        if (_characters.Count < _maxCapacity)
        {
            _capacity++;
            int indexPoint = _characters.Count;
            Animal character = Instantiate(_animalData.AnimalPrefab, _charactersPositions[indexPoint].position, Quaternion.identity, _parent);
            character.Init(this);
            character.GetComponent<MovementHandler>().Init(_inputType, _charactersPositions[indexPoint], _animalData.Speed);
            _characters.Add(character);
        }
    }

    private void SetPositions()
    {
        Transform[] positions = _positions.gameObject.GetComponentsInChildren<Transform>();
        Debug.Log(positions.Length);
        _maxCapacity = positions.Length;

        foreach (Transform position in positions)
        {
            if(position != _positions)
            {
                _charactersPositions.Add(position);
            }
        }
    }

    private void Create()
    {
        for (int i = 0; i < _capacity; i++)
        {
            AddCharacter(i);
        }
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
}
