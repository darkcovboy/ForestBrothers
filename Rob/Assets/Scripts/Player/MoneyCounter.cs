using System;
using Zenject;

public class MoneyCounter : IMoneyChangedHandler
{
    public event Action<int> OnMoneyChanged;

    public int EarnedMoney => _earnedMoney;

    private int _money;
    private int _earnedMoney;
    private int _startMoney;
    private PlayerSave _playerSave;

    [Inject]
    public void Constructor(PlayerSave playerSave)
    {
        _playerSave = playerSave;
    }

    public MoneyCounter(int startMoney)
    {
        _money = startMoney;
        _startMoney = startMoney;
        _earnedMoney = 0;
        OnMoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int money)
    {
        if (money <= 0)
            throw new ArgumentException();

        _money += money;
        _earnedMoney += money;
        _playerSave.UpdateMoney(_money);
        OnMoneyChanged?.Invoke(_money);
    }

    public bool IsEnough(int price)
    { 
        return _money > price;
    }

    public void Spend(int price)
    {
        if(price < 0)
            throw new ArgumentException();

        _money -= price;
        _playerSave.UpdateMoney(_money);
        OnMoneyChanged?.Invoke(_money);
    }

    public void UpdateMoney()
    {
        OnMoneyChanged?.Invoke(_money);
    }

    public int GetMoney()
    {
        return _money;
    }
}
