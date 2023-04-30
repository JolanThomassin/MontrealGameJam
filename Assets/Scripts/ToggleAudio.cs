using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic, _toggleEffects;

    public Sprite volUpImage;
    public Sprite volDownImage;

    public void Toggle() {
        if (_toggleEffects) SoundManager.Instance.ToggleEffects();
        if (_toggleMusic) SoundManager.Instance.ToggleMusic();

        if (_toggleMusic) {
            if (SoundManager.Instance._musicSource.mute) {
                // Si le son est coupé, changer l'image du bouton en "volDownImage"
                this.GetComponent<Image>().sprite = volDownImage;
            } else {
                // Sinon, changer l'image du bouton en "volUpImage"
                this.GetComponent<Image>().sprite = volUpImage;
            }
        } else if (_toggleEffects) {
            if (SoundManager.Instance._effectsSource.mute) {
                // Si le son est coupé, changer l'image du bouton en "volDownImage"
                this.GetComponent<Image>().sprite = volDownImage;
            } else {
                // Sinon, changer l'image du bouton en "volUpImage"
                this.GetComponent<Image>().sprite = volUpImage;
            }
        }
        
    }

}
