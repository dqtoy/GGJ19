using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation : MonoBehaviour
{
    // gridX and gridY are 0-based
    public int m_GridX = 0;
    public int m_GridY = 0;

    // Cached Variables
    private BoardPanel boardPanel;
    private RectTransform rectTransform;
    private RectTransform boardRectTransform;

    void GetCachedMembers()
    {
        boardPanel = GetComponentInParent<BoardPanel>();
        rectTransform  = gameObject.GetComponent<RectTransform>();
        if (boardPanel != null)
        {
            boardRectTransform = boardPanel.gameObject.GetComponent<RectTransform>();
        }
    }
    
    public void SnapToGrid()
    {
        if (boardPanel == null)
        {
            GetCachedMembers();
        }
        
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        float gridWidth = boardRectTransform.rect.width / boardPanel.NumColumns;
        float gridHeight = boardRectTransform.rect.height / boardPanel.NumRows;
        
        rectTransform.anchoredPosition = new Vector2(gridWidth * m_GridX + width / 2, - (gridHeight * m_GridY + height / 2));
    }
}
