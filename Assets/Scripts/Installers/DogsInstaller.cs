using SunnyDoggyClicker.UI.View.Dogs;
using SunnyDoggyClicker.UI.ViewModel;
using UnityEngine;
using Zenject;

public class DogsInstaller : MonoInstaller {
    [SerializeField] private DogsView _dogsView;
    public override void InstallBindings() {
        Container.Bind<DogsViewModel>().AsSingle();
        _dogsView.Bind(Container.Resolve<DogsViewModel>());
        Container.BindInstance(_dogsView);
    }
}
