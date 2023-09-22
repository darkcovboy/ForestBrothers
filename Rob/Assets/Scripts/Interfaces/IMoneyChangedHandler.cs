using UnityEngine;
using UnityEngine.Events;

public interface IMoneyChangedHandler
{
    public event UnityAction<int> OnMoneyChanged;
}
