using SunnyDoggyClicker;
using SunnyDoggyClicker.UI.View.ClickerView;
using SunnyDoggyClicker.UI.ViewModel;
using UnityEngine;
using Zenject;

public class ClickerInstaller : MonoInstaller {
    [SerializeField] private ClickerSettings _settings;
    [SerializeField] private ClickerView _clickerView;

    public override void InstallBindings() {
        Container.Bind<ClickerSettings>().FromInstance(_settings);
        Container.Bind<ClickerModel>().AsSingle();
        Container.Bind<ClickerViewModel>().AsSingle();
        _clickerView.Bind(Container.Resolve<ClickerViewModel>());
        Container.BindInstance<ClickerView>(_clickerView).AsSingle();
    }
}
