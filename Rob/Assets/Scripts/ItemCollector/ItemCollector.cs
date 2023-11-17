using UnityEngine;
using Zenject;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour, IItemChangedHandler, IGameResultHandler
{
    [SerializeField] private int _countItems = 0;
    [SerializeField] private Transform _pointToAttach;
    [SerializeField] private RewardDisplayPool _rewardDisplayPool;
    public Transform Point => _pointToAttach;

    public event UnityAction<int, int> OnCountItemsChanged;
    public event UnityAction OnGameWinning;

    private MoneyCounter _moneyCounter;
    private int _countItemsMax;
    private readonly float _percentage = 0.9f;
    private Vector3 _colliderSize;

    private void Start()
    {
        _colliderSize = gameObject.GetComponent<BoxCollider>().size;
        OnCountItemsChanged?.Invoke(_countItems, _countItemsMax);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Animal>(out Animal animal))
        {
            if(animal.IsItemHandled == true)
            {
                Item item = animal.GetItem();
                AddItem(item.Reward);
                item.ConnecTo(this);
            }
        }
    }

    [Inject]
    public void Constructor(MoneyCounter moneyCounter, ItemKeeper itemKeeper)
    {
        _moneyCounter = moneyCounter;
        _countItemsMax = (int)(itemKeeper.CalculateItems() * _percentage);
    }


    private void AddItem(int reward)
    {
        _moneyCounter.AddMoney(reward);
        _countItems++;
        _rewardDisplayPool.ShowTextPopup(reward);
        OnCountItemsChanged?.Invoke(_countItems, _countItemsMax);

        if(_countItems >= _countItemsMax)
        {
            OnGameWinning?.Invoke();
            _colliderSize = _colliderSize * 2;
            gameObject.GetComponent<BoxCollider>().size = _colliderSize;
        }
    }
}
