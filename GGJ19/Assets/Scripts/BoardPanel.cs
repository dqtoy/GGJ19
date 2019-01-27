using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BoardPanel : Singleton<BoardPanel>
{
    public int NumRows = 10;
    public int NumColumns = 14;
    public int[,] boarddata;

    public Player m_Player1;
    public GridLocation m_Cursor;

    public PlayerPanel m_Player1Panel;
    public PlayerPanel m_Player2Panel;

    public Transform m_PipesRoot;

    private PipeSection[,] m_LaidPipes;
    
    // Start is called before the first frame update
    void Start()
    {
        m_LaidPipes = new PipeSection[NumColumns,NumRows];
    }


    void Update()
    {
        if (GameplayManager.Instance.gameState != GameState.Playing)
            return;
        
        CheckForInput();
    }


    public void InitBoard(int[,] data, int startId, int endId)
    {
        boarddata = data;
    }

    void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Move left if possible
            SetNewPositionIfPossible(m_Cursor.m_GridX - 1, m_Cursor.m_GridY, m_Cursor);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Move left if possible
            SetNewPositionIfPossible(m_Cursor.m_GridX, m_Cursor.m_GridY + 1, m_Cursor);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Move left if possible
            SetNewPositionIfPossible(m_Cursor.m_GridX + 1, m_Cursor.m_GridY, m_Cursor);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Move left if possible
            SetNewPositionIfPossible(m_Cursor.m_GridX, m_Cursor.m_GridY - 1, m_Cursor);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPieceIfPossible(m_Cursor, m_Player1Panel);
        }
    }

    void SpawnPieceIfPossible(GridLocation spawnLocation, PlayerPanel playerToSpawnFrom)
    {
        // Need to check if there's a pipe there already
        Transform piece = playerToSpawnFrom.TakePiece();
        if (piece != null)
        {
            piece.SetParent(m_PipesRoot);
            GridLocation gridLocation = piece.GetComponent<GridLocation>();
            gridLocation.m_GridX = spawnLocation.m_GridX;
            gridLocation.m_GridY = spawnLocation.m_GridY;
            gridLocation.SnapToGrid();

            m_LaidPipes[spawnLocation.m_GridX, spawnLocation.m_GridY] = piece.GetComponent<PipeSection>();
        }
    }

    void SetNewPositionIfPossible(int gridX, int gridY, GridLocation cursor)
    {
        if (!IsValidPosition(gridX, gridY))
        {
            return;
        }

        cursor.m_GridX = gridX;
        cursor.m_GridY = gridY;
        cursor.SnapToGrid();
    }

    bool IsValidPosition(int gridX, int gridY)
    {
        if (gridX < 0)
        {
            return false;
        }
        if (gridY < 0)
        {
            return false;
        }
        if (gridX >= NumColumns)
        {
            return false;
        }
        if (gridY >= NumRows)
        {
            return false;
        }

        return true;
    }
}
