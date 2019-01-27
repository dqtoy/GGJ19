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
    public float moveSpeed;
    public Queue<Vector3> wayPoints;

    private CharacterState characterState = CharacterState.Preparing;
    private PipeSection currentTile;


	private void Update()
	{
        if (characterState != CharacterState.Moving)
            return;
	}




    public void Init(PipeSection startTile)
    {
        currentTile = startTile;
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

        currentTile.OnPlayerEnter(0);

        //setup waypoints
        wayPoints = PipeUtils.GetWayPoints(currentTile, currentTile.characterEntry, currentTile.characterExit);
    }

}
