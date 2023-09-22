using UnityEngine;
using Zenject;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class MoneyBalance : MonoBehaviour
{
    private MoneyCounter _moneyCounter;
    private TMP_Text _text;

    [Inject]
    public void Constructor(MoneyCounter moneyCounter)
    {
        _moneyCounter = moneyCounter;
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _moneyCounter.OnMoneyChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _moneyCounter.OnMoneyChanged -= OnValueChanged;
    }

    private void OnValueChanged(int money)
    {
        _text.text = money.ToString();
    }
}
