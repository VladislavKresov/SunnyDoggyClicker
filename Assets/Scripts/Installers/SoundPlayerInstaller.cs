using SunnyDoggyClicker.Audio;
using UnityEngine;
using Zenject;

public class SoundPlayerInstaller : MonoInstaller {
    [SerializeField] private SoundPlayer _soundPlayer;

    public override void InstallBindings() {
        Container.BindInstances(_soundPlayer);
    }
}
