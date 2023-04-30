using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource _musicSource, _effectsSource;

    private float _musicFadeTime = 10.0f;
    private float _musicFadeTimer = 0.0f;
    private bool isChanged = true;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (isChanged) {
            // Vérifie si la musique doit être montée
            if (_musicFadeTimer < _musicFadeTime) {
                // Met à jour le timer de fondu
                _musicFadeTimer += Time.deltaTime;

                // Calcule la nouvelle valeur de volume en utilisant la méthode Lerp
                float volume = Mathf.Lerp(0.0f, 1.0f, _musicFadeTimer / _musicFadeTime);

                // Met à jour le volume de la musique
                _musicSource.volume = volume;
            }
        }
    }

    public void PlaySoundEffects(AudioClip clip) {
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value) {
        AudioListener.volume = value;  
    }

    public void ChangeMusicVolume(float value) {
        isChanged = false;
        _musicSource.volume = value;  
    }

    public void ChangeEffectsVolume(float value) {
        _effectsSource.volume = value;
    }

    public void ToggleEffects() {
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic() {
        _musicSource.mute = !_musicSource.mute;
    }
}
