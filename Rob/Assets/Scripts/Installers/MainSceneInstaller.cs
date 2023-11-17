using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private LocalizationDeterminate _localization;
    [SerializeField] private Loader _loaderPrefab;

    public override void InstallBindings()
    {
        Container.Bind<LocalizationDeterminate>().FromInstance(_localization);
        Loader loader = Container.InstantiatePrefabForComponent<Loader>(_loaderPrefab);
        Container.Bind<Loader>().FromInstance(loader).AsSingle();
    }
}
