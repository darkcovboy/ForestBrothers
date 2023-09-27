using System.Linq;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerStartPositon;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private ItemCollector _itemCollector;
    [SerializeField] private ItemKeeper _itemKeeper;
    [SerializeField] private SkinsContainer _skinsContainer;

    private MoneyCounter _moneyCounter;
    private PlayerSave _playerSave;
    public override void InstallBindings()
    {
        BindStart();
        BindMovement();
        BindPlayer();
        BindUI();
    }

    private void BindStart()
    {
        _playerSave = new PlayerSave(_skinsContainer);
        Container.Bind<PlayerSave>().FromInstance(_playerSave).AsSingle();
        _moneyCounter = new MoneyCounter(_playerSave.SaveData.Money);
        Container.Bind<MoneyCounter>().FromInstance(_moneyCounter).AsCached();
        Container.Bind<ItemKeeper>().FromInstance(_itemKeeper).AsSingle();
    }

    private void BindUI()
    {
        Container.Bind<IMoneyChangedHandler>().To<MoneyCounter>().FromInstance(_moneyCounter).AsSingle();
        Container.Bind<IItemChangedHandler>().To<ItemCollector>().FromInstance(_itemCollector).AsSingle();
        _moneyCounter.UpdateMoney();
    }

    private void BindMovement()
    {
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
        }
        else
        {
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
        }
    }

    private void BindPlayer()
    {
        Container.Bind<Player>().FromMethod(CreatePlayer).AsSingle();
    }

    private Player CreatePlayer()
    {
        Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerStartPositon.position, Quaternion.identity, null);
        AnimalData animalData = _skinsContainer.AnimalData.First<AnimalData>(x => x.AnimalType == _playerSave.SaveData.SelectedSkin);
        player.Initialize(_playerSave.SaveData.Capacity, animalData);
        return player;
    }
}
