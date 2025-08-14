using UnityEngine;

namespace SunnyDoggyClicker.Audio {
    public class SoundPlayer : MonoBehaviour {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClips;

        public void Play(int sound) {
            _audioSource.clip = _audioClips[sound];
            _audioSource.Play();
        }
    }
}