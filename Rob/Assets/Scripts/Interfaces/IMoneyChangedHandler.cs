using System;

public interface IMoneyChangedHandler
{
    public event Action<int> OnMoneyChanged;

    public int GetMoney();
}
