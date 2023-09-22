using UnityEngine;
using System;
using UnityEngine.Events;

public class MoneyCounter : MonoBehaviour, IMoneyChangedHandler
{
    public event UnityAction<int> OnMoneyChanged;

    private int _money;

    private void Start()
    {
        OnMoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int money)
    {
        if (money <= 0)
            throw new ArgumentException();

        _money += money;
        OnMoneyChanged?.Invoke(_money);
    }
}
