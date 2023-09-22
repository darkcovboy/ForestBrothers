using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerStartPositon;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private MoneyCounter _moneyCounter;
    [SerializeField] private ItemCollector _itemCollector;

    public override void InstallBindings()
    {
        BindMovement();
        BindPlayer();
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
        Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerStartPositon.position, Quaternion.identity, null);
        _cameraFollow.Constructor(player);
        Container.Bind<MoneyCounter>().FromInstance(_moneyCounter).AsSingle();
        Container.Bind<ItemCollector>().FromInstance(_itemCollector).AsSingle();
    }
}
