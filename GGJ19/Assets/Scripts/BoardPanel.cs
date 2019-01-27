using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPanel : MonoBehaviour
{
    public int NumRows = 11;
    public int NumColumns = 14;

    public Player m_Player1;
    public GridLocation m_Cursor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
    }

    void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Move left if possible
            SetNewPositionIfPossible(m_Cursor.m_GridX - 1, m_Cursor.m_GridY);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Move left if possible
            SetNewPositionIfPossible(m_Cursor.m_GridX, m_Cursor.m_GridY + 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Move left if possible
            SetNewPositionIfPossible(m_Cursor.m_GridX + 1, m_Cursor.m_GridY);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Move left if possible
            SetNewPositionIfPossible(m_Cursor.m_GridX, m_Cursor.m_GridY - 1);
        }
    }

    void SetNewPositionIfPossible(int gridX, int gridY)
    {
        if (!IsValidPosition(gridX, gridY))
        {
            return;
        }

        m_Cursor.m_GridX = gridX;
        m_Cursor.m_GridY = gridY;
        m_Cursor.SnapToGrid();
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
