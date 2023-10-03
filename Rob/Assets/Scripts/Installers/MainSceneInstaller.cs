using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private MainScenePanel _mainScenePanel;
    [SerializeField] private Loader _loaderPrefab;

    public override void InstallBindings()
    {
        PlayerSave playerSave = new PlayerSave();
        Loader loader = Container.InstantiatePrefabForComponent<Loader>(_loaderPrefab);
        loader.SetNextLevel(playerSave.SaveData.LastLevelId);
        Container.Bind<Loader>().FromInstance(loader).AsSingle();
    }
}
