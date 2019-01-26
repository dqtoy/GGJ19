using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPanel : MonoBehaviour
{
    public int NumRows = 11;
    public int NumColumns = 14;

    public Player Player1;
    
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
            SetNewPositionIfPossible(Player1.gridX - 1, Player1.gridY);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Move left if possible
            SetNewPositionIfPossible(Player1.gridX, Player1.gridY + 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Move left if possible
            SetNewPositionIfPossible(Player1.gridX + 1, Player1.gridY);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Move left if possible
            SetNewPositionIfPossible(Player1.gridX, Player1.gridY - 1);
        }
    }

    void SetNewPositionIfPossible(int gridX, int gridY)
    {
        if (!IsValidPosition(gridX, gridY))
        {
            return;
        }

        Player1.gridX = gridX;
        Player1.gridY = gridY;

        RectTransform playerRectTransform = Player1.gameObject.GetComponent<RectTransform>();
        RectTransform boardRectTransform = gameObject.GetComponent<RectTransform>();
        
        float width = playerRectTransform.rect.width;
        float height = playerRectTransform.rect.height;
        float gridWidth = boardRectTransform.rect.width / NumColumns;
        float gridHeight = boardRectTransform.rect.height / NumRows;
        
        Vector3 localPosition = new Vector3(gridWidth * gridX + width / 2, - (gridHeight * gridY + height / 2), 0);
        Player1.gameObject.transform.localPosition = localPosition;
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
