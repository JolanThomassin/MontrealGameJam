using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public bool mainSound;
    public bool musicSound;
    public bool effectsSound;

    void Start()
    {
        if (mainSound) {
            float currentVolume = AudioListener.volume;
            _slider.value = currentVolume;
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
        }

        if (musicSound) {
            float currentVolume = SoundManager.Instance._musicSource.volume;
            _slider.value = currentVolume;
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        }

        if (effectsSound) {
            float currentVolume = SoundManager.Instance._effectsSource.volume;
            _slider.value = currentVolume;
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeEffectsVolume(val));
        }
        
    }
}
