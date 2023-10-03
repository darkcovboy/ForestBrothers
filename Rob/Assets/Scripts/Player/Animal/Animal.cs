using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class Animal : MonoBehaviour
{
    [SerializeField] private Transform _pointHandler;
    [SerializeField] private ItemFinder _itemFinder;
    [SerializeField] private AnimationSwitcher _switcher;
    public bool IsItemHandled => _item != null;
    public bool IsDiyng => _isDiyng;
    public Transform PointHandler => _pointHandler;

    public event UnityAction OnDiyng;
    public event UnityAction OnCelebration;

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

    public void Win()
    {
        OnCelebration?.Invoke();
    }

    [Button]
    public void Remove()
    {
        _itemFinder.gameObject.Deactivate();
        _player.RemoveCharacter(this);

        if(_item != null)
        {
            _item.Disconnect();
            _item = null;
        }
        
        OnDiyng?.Invoke();
    }
}
