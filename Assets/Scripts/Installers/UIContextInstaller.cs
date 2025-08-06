using SunnyDoggyClicker.UI;
using Zenject;

public class UIContextInstaller : MonoInstaller {

    public override void InstallBindings() {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ShowViewSignal>();
        Container.DeclareSignal<HideViewSignal>();
    }
}
