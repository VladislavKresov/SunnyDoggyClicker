using SunnyDoggyClicker.UI.View.Weather;
using SunnyDoggyClicker.UI.ViewModel;
using UnityEngine;
using Zenject;

public class WeatherInstaller : MonoInstaller {
    [SerializeField] private WeatherView _weatherView;

    public override void InstallBindings() {
        Container.Bind<WeatherViewModel>().AsSingle();
        _weatherView.Bind(Container.Resolve<WeatherViewModel>());
        Container.BindInstance(_weatherView);
    }
}
