using System;

public class MoneyCounter : IMoneyChangedHandler
{
    public event Action<int> OnMoneyChanged;

    private int _money;
    private int _startMoney;

    public MoneyCounter(int startMoney)
    {
        _money = startMoney;
        _startMoney = startMoney;
        OnMoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int money)
    {
        if (money <= 0)
            throw new ArgumentException();

        _money += money;
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
