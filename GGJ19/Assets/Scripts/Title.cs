using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TitleState
{
    FadeIn,
    Idle,
    FadeOut
};

public class Title : MonoBehaviour
{
    public FadeInOut Text;
    private bool clicked = false;

	private void Start()
	{
        //Text.Play();
	}

	private void OnMouseDown()
	{
        if (clicked)
            return;

        clicked = true;
        Text.Play();
        GameplayManager.Instance.StartGame();
	}
}
