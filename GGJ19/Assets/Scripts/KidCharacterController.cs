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
    private Tile currentTile;
    private Vector3 targetPosition;


	private void Update()
	{
        if (characterState != CharacterState.Moving)
            return;

        if (Vector3.Distance(transform.position, targetPosition)  < 0.001f)
        {
            if (wayPoints.Count == 0)
                OnReachTileEixt();
        }
	}


    public void Init(Tile startTile)

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
        //kidCharacter.SetActive(true);

        currentTile.OnPlayerEnter(0);

        //setup waypoints
        wayPoints = PipeUtils.GetWayPoints(currentTile, currentTile.characterEntry, currentTile.characterExit);
        targetPosition = wayPoints.Dequeue();
    }

    private void OnReachTileEixt()
    {
        //get next tile
        GridLocation gridLocation = currentTile.GetComponent<GridLocation>();
        int x = gridLocation.m_GridX;
        int y = gridLocation.m_GridY;
        if (currentTile.characterExit == 0)
            x -= 1;
        else if (currentTile.characterExit == 1)
            y += 1;
        else if (currentTile.characterExit == 2)
            x += 1;
        else if (currentTile.characterExit == 3)
            y -= 1;

        if (!BoardPanel.Instance.IsValidPosition(x, y))
            GameplayManager.Instance.Lose();



    }

}
