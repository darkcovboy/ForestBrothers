using System.Linq;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller, ICoroutineRunner
{
    [Header("Containers")]
    [SerializeField] private GamePrefabsContainer _gamePrefabsContainer;
    [SerializeField] private SkinsContainer _skinsContainer;
    [SerializeField] private SaveStartData _saveStartData;
    [Header("Positions")]
    [SerializeField] private Transform _playerStartPositon;
    [SerializeField] private Transform _itemCollectorPosition;
    [Header("Enemy(Can be null)")]
    [SerializeField] private Transform _enemyPosition;
    [SerializeField] private Transform _containerForEnemyPointer;
    [Header("Objects")]
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private ItemKeeper _itemKeeper;
    [SerializeField] private LocalizationDeterminate _localizationDeterminate;

    private MoneyCounter _moneyCounter;
    private PlayerSave _playerSave;
    private Loader _loader;
    private Player _player;
    private ItemCollector _itemCollector;

    public override void InstallBindings()
    {
        BindStart();
        BindMovement();
        BindPlayer();
        BindUI();
        BindEnemy();
    }

    private void BindEnemy()
    {
        if(_gamePrefabsContainer.HaveEnemy)
        {
            EnemyPointerView enemyPointerView = Container.InstantiatePrefabForComponent<EnemyPointerView>(_gamePrefabsContainer.EnemyPointerView, _containerForEnemyPointer);
            Container.Bind<EnemyPointerView>().FromInstance(enemyPointerView).AsSingle();
            Enemy enemy = Container.InstantiatePrefabForComponent<Enemy>(_gamePrefabsContainer.Enemy, _enemyPosition.position, Quaternion.identity, null);
            Container.Bind<Enemy>().FromInstance(enemy).AsSingle();
        }
    }

    private void BindStart()
    {
#if UNITY_EDITOR
        _playerSave = new PlayerSave(this);
#elif UNITY_WEBGL
        _playerSave = PlayerSaveContainer.PlayerSave;
#endif
        _playerSave.SkinContainer = _skinsContainer;
        _moneyCounter = new MoneyCounter(_playerSave.SaveData.Money);
        _moneyCounter.Constructor(_playerSave);
        Container.Bind<MoneyCounter>().FromInstance(_moneyCounter);
        Container.Bind<Loader>().FromInstance(Loader.Instance).AsSingle();
        Container.Bind<PlayerSave>().FromInstance(_playerSave).AsSingle();
        RewardedVideo rewardedVideo = new RewardedVideo();
        FullVideo fullVideo = new FullVideo();
        rewardedVideo.Constructor(_moneyCounter);
        Container.Bind<RewardedVideo>().FromInstance(rewardedVideo).AsSingle();
        Container.Bind<FullVideo>().FromInstance(fullVideo).AsSingle();
        Container.Bind<ItemKeeper>().FromInstance(_itemKeeper).AsSingle();
        Container.Bind<LocalizationDeterminate>().FromInstance(_localizationDeterminate);
        _itemCollector = Container.InstantiatePrefabForComponent<ItemCollector>(_gamePrefabsContainer.ItemCollectorPrefab, _itemCollectorPosition.position, Quaternion.identity, null);
        Container.Bind<ItemCollector>().FromInstance(_itemCollector).AsSingle();
    }

    private void BindUI()
    {
        Container.Bind<IMoneyChangedHandler>().To<MoneyCounter>().FromInstance(_moneyCounter);
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
        Player player = Container.InstantiatePrefabForComponent<Player>(_gamePrefabsContainer.PlayerPrefab, _playerStartPositon.position, Quaternion.identity, null);
        AnimalData animalData = _skinsContainer.AnimalData.First<AnimalData>(x => x.AnimalType == _playerSave.SaveData.SelectedSkin);
        player.Initialize(_playerSave.SaveData.Capacity, animalData);
        _player = player;
        Debug.Log(player.name + " " + _player.name);
        return player;
    }
}
