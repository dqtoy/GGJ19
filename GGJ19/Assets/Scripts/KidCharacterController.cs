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
    public Queue<Vector2> wayPoints;

    private CharacterState characterState = CharacterState.Preparing;
    private Tile currentTile;
    private Vector2 targetPosition;


	private void Update()
	{
        if (characterState != CharacterState.Moving)
            return;

        RectTransform rectTransform = GetComponent<RectTransform>();
        float dist = Vector2.Distance(rectTransform.anchoredPosition, targetPosition);
        if (dist < 0.001f)
        {
            if (wayPoints.Count == 0)
            {
                OnReachTileEixt();
                return;
            }
            else
            {
                targetPosition = wayPoints.Dequeue();
                return;
            }
        }

        Vector2 direction = (targetPosition - rectTransform.anchoredPosition).normalized;
        float moveDist = Mathf.Min(moveSpeed, dist);
        rectTransform.anchoredPosition += direction * moveDist;


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
        kidCharacter.SetActive(true);

        currentTile.OnPlayerEnter(0);

        //setup waypoints
        //wayPoints = TileUtils.GetWayPoints(currentTile, currentTile.characterEntry, currentTile.characterExit);
        //UpdateWayPoint();
        targetPosition = wayPoints.Dequeue();
    }

    public void UpdateWayPoint()
    {
        wayPoints = TileUtils.GetWayPoints(currentTile, currentTile.characterEntry, currentTile.characterExit);
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
            y -= 1;
        else if (currentTile.characterExit == 2)
            x += 1;
        else if (currentTile.characterExit == 3)
            y += 1;

        if (!BoardPanel.Instance.IsValidPosition(x, y))
        {
            GameplayManager.Instance.Lose();
            return;
        }

        Tile neighborTile = BoardPanel.Instance.GetTile(x, y);
        if (neighborTile == null)
        {
            GameplayManager.Instance.Lose();
            return;
        }

        if (neighborTile == BoardPanel.Instance.m_ExitTile)
        {
            GameplayManager.Instance.Win();
            return;
        }

        int newEnterPoint = (currentTile.characterExit + 2) % 4;
        if (TileUtils.Connects(newEnterPoint, neighborTile))
        {
            currentTile.OnPlayerExit();
            currentTile = neighborTile;
            neighborTile.OnPlayerEnter(newEnterPoint);

        }
        else
        {
            GameplayManager.Instance.Lose();
            return;
        }


    }

}
