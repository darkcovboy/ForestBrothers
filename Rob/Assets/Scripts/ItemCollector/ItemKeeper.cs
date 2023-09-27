using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    [SerializeField] private int _itemsCount;
    public int CalculateItems() => transform.childCount;

    private void OnValidate()
    {
        _itemsCount = transform.childCount;
    }
}
