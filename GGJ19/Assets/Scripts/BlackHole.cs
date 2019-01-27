using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlackHole : MonoBehaviour
{
    private const float m_RotationsPerSecond = 0.1f;
    private const float m_NumSecondsBetweenMoves = 5f;
    private const float m_MoveTime = 5f;
    
    private float m_ElapsedTime = 0.0f;
    private float m_SecondsUntilNextMove = m_NumSecondsBetweenMoves;
    private float m_TimeOfLastMove = 0.0f;

    private int targetGridX;
    private int targetGridY;
    private GridLocation m_GridLocationComponent;
    private RectTransform m_RectTransformComponent;
    private Tile m_TileComponent;

    private Tile m_TileToSwallow = null;
    
    // Start is called before the first frame update
    void Start()
    {
        m_ElapsedTime = 0.0f;
        m_SecondsUntilNextMove = m_NumSecondsBetweenMoves;
        
        // We should start off with targetGrid location equal to gridLocation.m_GridX
        m_GridLocationComponent = GetComponent<GridLocation>();
        m_RectTransformComponent = GetComponent<RectTransform>();
        m_TileComponent = GetComponent<Tile>();
        
        targetGridX = m_GridLocationComponent.m_GridX;
        targetGridY = m_GridLocationComponent.m_GridY;
    }

    // Update is called once per frame
    void Update()
    {
        m_ElapsedTime += Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, (360 * m_RotationsPerSecond * m_ElapsedTime) % 360);

        if (targetGridX != m_GridLocationComponent.m_GridX ||
            targetGridY != m_GridLocationComponent.m_GridY)
        {
             Vector2 previousAnchoredPosition =
                m_GridLocationComponent.GetAnchoredPosition(m_GridLocationComponent.m_GridX, m_GridLocationComponent.m_GridY);
            Vector2 targetAnchoredPosition =
                m_GridLocationComponent.GetAnchoredPosition(targetGridX, targetGridY);

            Vector2 deltaForTimestep = (targetAnchoredPosition - previousAnchoredPosition) / (m_MoveTime / Time.deltaTime);
            Vector2 deltaToTarget = m_RectTransformComponent.anchoredPosition - targetAnchoredPosition;
            if (deltaForTimestep.sqrMagnitude > deltaToTarget.sqrMagnitude)
            {
                // We're almost there so just snap to the location
                BoardPanel.Instance.RemoveFromBoard(m_TileComponent);
                BoardPanel.Instance.AddToBoard(m_TileComponent, targetGridX, targetGridY);
                m_RectTransformComponent.SetAsFirstSibling(); // Make sure it's underneath the others
                m_TileToSwallow = null;
                return;
            }
            else
            {
                // Move towards the target location
                m_RectTransformComponent.anchoredPosition += deltaForTimestep;
            }

            m_TileToSwallow = BoardPanel.Instance.GetTile(targetGridX, targetGridY); // Check if a tile was added
            if (m_TileToSwallow != null)
            {
                GridLocation gl = m_TileToSwallow.GetComponent<GridLocation>();
                if (Mathf.Abs(gl.m_GridX - m_GridLocationComponent.m_GridX) > 1 ||
                    Mathf.Abs(gl.m_GridY - m_GridLocationComponent.m_GridY) > 1)
                {
                    // hacky fix for a bug which targets tiles in the top left corner for some reason.
                    m_TileToSwallow = null;
                    return;
                }
                
                // Shrink it and squash/stretch it a bit
                float t = (m_ElapsedTime - m_TimeOfLastMove) / m_MoveTime; // t goes from 0..1
                float ramp = 0.5f;
                float sinAmplitude = 0.2f * t;
                float sinFrequency = 1;
                float sinOffset = Mathf.Sin((m_ElapsedTime - m_TimeOfLastMove) * 2 * Mathf.PI * sinFrequency) * sinAmplitude;

                m_TileToSwallow.transform.localScale = 
                    new Vector3(
                        (1+sinOffset) * Mathf.Pow(1-t, ramp), 
                        (1-sinOffset) * Mathf.Pow(1-t, ramp), 
                        1);
            }
            
            return;
        }

        m_SecondsUntilNextMove -= Time.deltaTime;
        if (m_SecondsUntilNextMove <= 0)
        {
            // Pick a square next to the black hole and move towards it
            FindSquareToMoveTo();
            m_TileToSwallow = BoardPanel.Instance.GetTile(targetGridX, targetGridY);
            m_SecondsUntilNextMove = m_NumSecondsBetweenMoves;
            m_TimeOfLastMove = m_ElapsedTime;
        }
    }

    void FindSquareToMoveTo()
    {
        int random = Random.Range(0, 9);
        targetGridX = m_GridLocationComponent.m_GridX + (random % 3) - 1;
        targetGridY = m_GridLocationComponent.m_GridY + (random / 3) - 1;
        bool canAccept = BoardPanel.Instance.CanAcceptTile(targetGridX, targetGridY);

        while (canAccept == false)
        {
            random = Random.Range(0, 9);
            targetGridX = m_GridLocationComponent.m_GridX + (random % 3) - 1;
            targetGridY = m_GridLocationComponent.m_GridY + (random / 3) - 1;
            canAccept = BoardPanel.Instance.CanAcceptTile(targetGridX, targetGridY);
        }
    }
}
