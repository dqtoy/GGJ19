using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    [Header("Background Music")]
    public AudioSource audioSource_BG;
    public AudioClip audioClip_BG;



    public void PlayBGMusic()
    {
        audioSource_BG.clip = audioClip_BG;
        audioSource_BG.loop = true;
        audioSource_BG.Play();
    }

    private void Start()
    {
        PlayBGMusic();
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game2");
        }
	}
   
}


