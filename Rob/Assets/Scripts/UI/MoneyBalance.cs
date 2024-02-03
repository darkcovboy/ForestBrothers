using UnityEngine;
using Zenject;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class MoneyBalance : MonoBehaviour
{
    private IMoneyChangedHandler _moneyChangedHandler;
    private TMP_Text _text;

    [Inject]
    public void Constructor(IMoneyChangedHandler moneyCounter)
    {
        _moneyChangedHandler = moneyCounter;
        _text = GetComponent<TMP_Text>();
        OnValueChanged(_moneyChangedHandler.GetMoney());
    }

    private void OnEnable()
    {
        _moneyChangedHandler.OnMoneyChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _moneyChangedHandler.OnMoneyChanged -= OnValueChanged;
    }

    private void OnValueChanged(int money)
    {
        _text.text = money.ToString();
    }
}
