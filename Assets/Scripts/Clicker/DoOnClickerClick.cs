using SunnyDoggyClicker.Signals;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class DoOnClickerClick : MonoBehaviour {
    public UnityEvent OnClickAction;
    [Inject] private readonly SignalBus _signalBus;

    private void Start() {
        _signalBus.Subscribe<ClickerClick>(OnClick);
    }

    private void OnClick() {
        OnClickAction?.Invoke();
    }
}
