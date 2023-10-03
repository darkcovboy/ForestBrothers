using System.Linq;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

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
    private Loader _loader;
    private Player _player;
    public override void InstallBindings()
    {
        BindStart();
        BindMovement();
        BindPlayer();
        BindUI();
    }

    private void BindStart()
    {
        _playerSave = new PlayerSave();
        _playerSave.SkinContainer = _skinsContainer;
        _loader = Loader.Instance;
        Container.Bind<Loader>().FromInstance(_loader).AsSingle();
        Container.Bind<PlayerSave>().FromInstance(_playerSave).AsSingle();
        _moneyCounter = new MoneyCounter(_playerSave.SaveData.Money);
        RewardedVideo rewardedVideo = new RewardedVideo();
        Container.Bind<MoneyCounter>().FromInstance(_moneyCounter).AsCached();
        Container.Bind<RewardedVideo>().FromInstance(rewardedVideo).AsSingle();
        Container.Bind<ItemKeeper>().FromInstance(_itemKeeper).AsSingle();
        Container.Bind<ItemCollector>().FromInstance(_itemCollector).AsSingle();
    }

    private void BindUI()
    {
        Container.Bind<IMoneyChangedHandler>().To<MoneyCounter>().FromInstance(_moneyCounter).AsSingle();
        Container.Bind<IItemChangedHandler>().To<ItemCollector>().FromInstance(_itemCollector);
        Container.Bind<IGameResultHandler>().To<ItemCollector>().FromInstance(_itemCollector);
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
        //Container.Bind<IGameLose>().To<Player>().FromInstance(_player);
    }

    private Player CreatePlayer()
    {
        Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerStartPositon.position, Quaternion.identity, null);
        AnimalData animalData = _skinsContainer.AnimalData.First<AnimalData>(x => x.AnimalType == _playerSave.SaveData.SelectedSkin);
        player.Initialize(_playerSave.SaveData.Capacity, animalData);
        _player = player;
        Debug.Log(player.name + " " + _player.name);
        return player;
    }
}
