using SunnyDoggyClicker;
using UnityEngine;
using Zenject;

public class ClickerInstaller : MonoInstaller {
    [SerializeField] private ClickerSettings _settings;

    public override void InstallBindings() {
        Container.Bind<ClickerSettings>().FromInstance(_settings);
        Container.Bind<Clicker>().AsSingle().NonLazy();
    }
}
