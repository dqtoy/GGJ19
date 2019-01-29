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
    private static bool firstLaunch = true;

	private void Start()
	{
		if (!firstLaunch)
		{
			gameObject.SetActive(false);
		}
		else
		{
			Text.Play();
		}

		firstLaunch = false;
        GameplayManager.Instance.StartGame();
	}
}
