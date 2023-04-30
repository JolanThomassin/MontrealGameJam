using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Okokok : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    void Start()
    {
        SoundManager.Instance.PlaySoundEffects(_clip);
    }


}
