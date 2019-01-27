using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BoardPanel : Singleton<BoardPanel>
{
    public const int NumRows = 10;
    public const int NumColumns = 14;

    public Player m_Player1;
    public GridLocation m_Cursor1;
    public GridLocation m_Cursor2;

    public PlayerPanel m_Player1Panel;
    public PlayerPanel m_Player2Panel;

    public Transform m_PipesRoot;

    private PipeSection[,] m_LaidPipes;
    public PipeSection m_EntryTile;
    public PipeSection m_ExitTile;
    private float tileLength;

    public void SetEntryExit(
        PipeSection entryTile, int entryGridX, int entryGridY, 
        PipeSection exitTile, int exitGridX, int exitGridY)
    {
        m_EntryTile = entryTile;
        AddToBoard(entryTile, entryGridX, entryGridY);
        
        m_ExitTile = exitTile;
        AddToBoard(exitTile, exitGridX, exitGridY);
    }

    public void AddToBoard(PipeSection tile, int gridX, int gridY)
    {
        tile.gameObject.transform.SetParent(m_PipesRoot);
        GridLocation gridLocation = tile.GetComponent<GridLocation>();
        gridLocation.m_GridX = gridX;
        gridLocation.m_GridY = gridY;
        gridLocation.SnapToGrid();

        m_LaidPipes[gridX, gridY] = tile;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        m_LaidPipes = new PipeSection[NumColumns, NumRows];
    }

    void Update()
    {
        if (GameplayManager.Instance.gameState != GameState.Playing)
            return;
        
        CheckForInput();
    }

    void CheckForInput()
    {
        // Player 1 Input
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left if possible
                SetNewPositionIfPossible(m_Cursor1.m_GridX - 1, m_Cursor1.m_GridY, m_Cursor1);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down if possible
                SetNewPositionIfPossible(m_Cursor1.m_GridX, m_Cursor1.m_GridY + 1, m_Cursor1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right if possible
                SetNewPositionIfPossible(m_Cursor1.m_GridX + 1, m_Cursor1.m_GridY, m_Cursor1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up if possible
                SetNewPositionIfPossible(m_Cursor1.m_GridX, m_Cursor1.m_GridY - 1, m_Cursor1);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SpawnPieceIfPossible(m_Cursor1, m_Player1Panel);
            }
        }
        
        // Player 1 Input
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Move left if possible
                SetNewPositionIfPossible(m_Cursor2.m_GridX - 1, m_Cursor2.m_GridY, m_Cursor2);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Move down if possible
                SetNewPositionIfPossible(m_Cursor2.m_GridX, m_Cursor2.m_GridY + 1, m_Cursor2);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Move right if possible
                SetNewPositionIfPossible(m_Cursor2.m_GridX + 1, m_Cursor2.m_GridY, m_Cursor2);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Move up if possible
                SetNewPositionIfPossible(m_Cursor2.m_GridX, m_Cursor2.m_GridY - 1, m_Cursor2);
            }

            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.RightAlt))
            {
                SpawnPieceIfPossible(m_Cursor2, m_Player2Panel);
            }
        }
    }

    void SpawnPieceIfPossible(GridLocation spawnLocation, PlayerPanel playerToSpawnFrom)
    {
        PipeSection existingTile = m_LaidPipes[spawnLocation.m_GridX, spawnLocation.m_GridY];
        if (existingTile != null && existingTile.m_Type == PipeSection.Type.NonRemoveableObstacle)
        {
            return;
        }
        
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

    public bool IsValidPosition(int gridX, int gridY)
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

    public PipeSection GetPipeSection(int gridX, int gridY)
    {
        if (!IsValidPosition(gridX, gridY))
            return null;

        return m_LaidPipes[gridX, gridY];
    }
}
