using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MultiplyAnimalsButton : MonoBehaviour
{
    private const string MaxLevel = "Max";

    [SerializeField] private Button _selfButton;
    [SerializeField] private Price _priceText;
    [SerializeField] private List<int> _prices;

    private MoneyCounter _moneyCounter;
    private Player _player;
    private int _currentLevel;
    private int _maxLevel;

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(MultiplyCharacters);
        Check();
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveListener(MultiplyCharacters);
    }

    [Inject]
    public void Constructor(MoneyCounter moneyCounter, Player player)
    {
        _moneyCounter = moneyCounter;
        _player = player;
        _currentLevel = _player.Capacity;
        _maxLevel = _player.MaxCapacity;
    }

    private void Check()
    {
        if (_currentLevel == _maxLevel)
        {
            _priceText.Show(MaxLevel);
            _selfButton.interactable = false;
        }
        else
        {
            _priceText.Show(_prices[_currentLevel]);
        }


        if (!_moneyCounter.IsEnough(_prices[_currentLevel]))
        {
            _selfButton.interactable = false;
        }
    }

    private void MultiplyCharacters()
    {
        _moneyCounter.Spend(_prices[_currentLevel]);
        _currentLevel++;
        _player.AddCharacter();
        Check();
    }
}
