using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation : MonoBehaviour
{
    // gridX and gridY are 0-based
    public int m_GridX = 0;
    public int m_GridY = 0;

    public float width;
    public float height;
    public float gridWidth;
    public float gridHeight;

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
        
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
        gridWidth = boardRectTransform.rect.width / BoardPanel.NumColumns;
        gridHeight = boardRectTransform.rect.height / BoardPanel.NumRows;
        
        rectTransform.anchoredPosition3D = new Vector3(gridWidth * m_GridX + width / 2, - (gridHeight * m_GridY + height / 2), 0);
        rectTransform.localScale = Vector3.one;
    }

    public Vector2 GetAnchoredPosition()
    {
        return new Vector2(gridWidth * m_GridX + width / 2, -(gridHeight * m_GridY + height / 2));
    }
}
