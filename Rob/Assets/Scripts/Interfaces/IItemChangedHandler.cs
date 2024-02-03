using UnityEngine;
using UnityEngine.Events;

public interface IItemChangedHandler
{
    public event UnityAction<int, int> OnCountItemsChanged;
}
