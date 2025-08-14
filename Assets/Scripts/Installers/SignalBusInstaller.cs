using SunnyDoggyClicker.Signals;
using Zenject;

public class SignalBusInstaller : MonoInstaller {
    public override void InstallBindings() {
        Zenject.SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ShowViewSignal>();
        Container.DeclareSignal<HideViewSignal>();
        Container.DeclareSignal<ClickerButtonUserClick>();
        Container.DeclareSignal<ClickerClick>();
        Container.DeclareSignal<LoadBreed>();
        Container.DeclareSignal<BreedLoading>();
        Container.DeclareSignal<BreedLoaded>();
    }
}
