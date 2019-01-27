using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Preparing,
    Moving,
    Win,
    Lose
};

public class KidCharacterController : Singleton<KidCharacterController>
{
    public GameObject kidCharacter;

    private CharacterState characterState = CharacterState.Preparing;
    //private int currentTile = ;


	private void Update()
	{
        if (characterState != CharacterState.Moving)
            return;


	}




	public void Init()
    {
        SpawnCharacter();
        StartMoving();
    }

    private void StartMoving()
    {
        characterState = CharacterState.Moving;
    }

    private void SpawnCharacter()
    {
        //spawn at spawn point
        kidCharacter.SetActive(true);
    }

}
