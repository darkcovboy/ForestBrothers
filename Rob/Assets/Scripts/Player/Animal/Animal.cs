using UnityEngine;
using Sirenix.OdinInspector;


public class Animal : MonoBehaviour
{
    [SerializeField] private Transform _pointHandler;
    [SerializeField] private ItemFinder _itemFinder;
    public bool IsItemHandled => _item != null;
    public bool IsDiyng => _isDiyng;
    public Transform PointHandler => _pointHandler;

    private Item _item;
    private Player _player;
    private bool _isDiyng = false;

    public void Init(Player player)
    {
        _player = player;
    }

    public void SetItem(Item item)
    {
        _item = item;
    }

    public Item GetItem()
    {
        Item itemToReturn = _item;
        _item = null;
        return itemToReturn;
    }

    [Button]
    private void Remove()
    {
        _isDiyng = true;
        _player.RemoveCharacter(this);
        //DestroyImmediate(this);
    }
}
