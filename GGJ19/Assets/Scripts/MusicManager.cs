using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [Header("Background Music")]
    public AudioSource audioSource_BG;
    public AudioClip audioClip_BG;
    public AudioClip audioClip_BGDangerous;
    public AudioClip audioClip_Win;
    public AudioClip audioClip_Lose;

    [Header("Sound Effect")]
    public AudioSource audioSource_SE;
    public AudioClip audioClip_Connect;
    public AudioClip audioClip_Disconnect;

    public void Awake()
    {
        GameplayManager.Instance.MusicManager = this;
    }

	public void PlayBGMusic()
    {
        audioSource_BG.clip = audioClip_BG;
        audioSource_BG.loop = true;
        audioSource_BG.Play();
    }


    public void PlayWinLevel()
    {
        audioSource_SE.clip = audioClip_Win;
        audioSource_SE.Play();
    }

    public void PlayLoseLevel()
    {
        audioSource_SE.clip = audioClip_Lose;
        audioSource_SE.Play();
    }

    public void PlayConnect()
    {
        audioSource_SE.clip = audioClip_Connect;
        audioSource_SE.Play();
    }


}
