using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAnimationSwitcher _enemyAnimationSwitcher;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private AnimalDetector _animalDetector;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _scaleDeacrease;
    [SerializeField] private float _durationMove;

    private Animal _currentAnimal;
    private IGameLose _gameLose;

    private void OnEnable()
    {
        _animalDetector.OnAnimalCatched += CatchAnimal;
    }

    private void OnDisable()
    {
        _animalDetector.OnAnimalCatched -= CatchAnimal;
        _gameLose.OnGameLossing -= OnGameLose;
    }

    [Inject]
    public void Constructor(Player player)
    {
        _gameLose = player;
        _gameLose.OnGameLossing += OnGameLose;
    }

    private void OnGameLose()
    {
        _animalDetector.gameObject.Deactivate();
        Debug.Log("Начал танцевать");
        _enemyMovement.StopMoving();
        _enemyMovement.enabled = false;
        _enemyAnimationSwitcher.PlayDanceAnimation();
    }

    private void CatchAnimal(Animal animal)
    {
        _currentAnimal = animal;
        _currentAnimal.transform.position = _target.position;
        _currentAnimal.Remove();
        _currentAnimal.transform.parent = _target;
        _enemyAnimationSwitcher.PlayAttackAnimation();
        _currentAnimal.transform.DOScale(_scaleDeacrease, _durationMove);
        StartCoroutine(OnAnimalDestroy());
    }

    private IEnumerator OnAnimalDestroy()
    {
        yield return new WaitForSeconds(3f);

        Destroy(_currentAnimal.gameObject);

        _currentAnimal = null;
    }
}
