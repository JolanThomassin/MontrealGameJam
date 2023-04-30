using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource _musicSource, _musicSourcePhase1, _musicSourcePhase2, _musicSourcePhase3, _musicSourcePhase4, _effectsSource;
    public bool inMenu;
    public bool inGame;
    private AudioSource currentSource;

    private float _musicFadeTime = 10.0f;
    private float _musicFadeTimer = 0.0f;
    private bool isChanged = true;
    private int phase = 0;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        ResetMusic();
    }

    void ResetMusic() {
        if (inGame) {
            currentSource = _musicSourcePhase1;
            _musicSourcePhase2.volume = 0;
            _musicSourcePhase3.volume = 0;
            _musicSourcePhase4.volume = 0;
            _musicSource.volume = 0;
        }
        if (inMenu) {
            currentSource = _musicSource;
            _musicSourcePhase1.volume = 0;
            _musicSourcePhase2.volume = 0;
            _musicSourcePhase3.volume = 0;
            _musicSourcePhase4.volume = 0;
        }
    }

    public void SwitchBool() {
        if (inMenu) {
            inMenu = false;
            inGame = true;
        } else {
            inMenu = true;
            inGame = false;
        }
        ResetMusic();
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
                currentSource.volume = volume;
            }
        }
    }

    public void CheckPhase(int numberPills, int numberNeeded) {
        float percentage = (float)numberPills / (float)numberNeeded;

        if (percentage < 0.25f)
        {
            if (phase == 0) {
                Debug.Log("Phase 1");
            }
            phase = 1;
        }
        else if (percentage < 0.5f)
        {
            if (phase == 1) {
                Debug.Log("Phase 2");
                _musicFadeTime = 10.0f;
                _musicFadeTimer = 0.0f;
                currentSource.volume = 1;
                currentSource = _musicSourcePhase2;
            }
            phase = 2;
        }
        else if (percentage < 0.75f)
        {
            if (phase == 2) {
                Debug.Log("Phase 3");
                _musicFadeTime = 10.0f;
                _musicFadeTimer = 0.0f;
                currentSource.volume = 1;
                currentSource = _musicSourcePhase3;
            }
            phase = 3;
        }
        else
        {
            if (phase == 3) {
                Debug.Log("Phase 4");
                _musicFadeTime = 10.0f;
                _musicFadeTimer = 0.0f;
                currentSource.volume = 1;
                currentSource = _musicSourcePhase4;
            }
            phase = 4;
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
