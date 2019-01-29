using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class Tile : MonoBehaviour
{
    public List<Image> m_DotConnectorImages;
    public Color m_DotCrossedColor;
    
    public enum Type
    {
        CrossPipe,
        StraightPipe,
        CurvedPipe,
        NonRemoveableObstacle
    };
    
    public enum PipeRotation
    {
        NoRotation,
        Clockwise90,
        Clockwise180,
        Clockwise270
    };

    public Type m_Type = Tile.Type.CrossPipe;
    public Tile.PipeRotation m_Orientation = PipeRotation.NoRotation;

    public FadeInOut m_CrossedFadeInOut;

    public int characterEntry = -1;
    public int characterExit = -1;

    /// <summary>
    /// Call when the player has crossed the tile
    /// </summary>
    public void SetTileCrossed()
    {
        foreach (Image image in m_DotConnectorImages)
        {
            image.CrossFadeColor(m_DotCrossedColor, m_CrossedFadeInOut.time, false, false);
        }

        m_CrossedFadeInOut.Play();
    }

    public void OnPlayerEnter(int enterPoint)
    {
        //calculate exit point
        characterEntry = enterPoint;
        characterExit = TileUtils.GetExitPoint(this, enterPoint);
        GameplayManager.Instance.KidCharacterController.UpdateWayPoint();

        SetTileCrossed();
    }

    public void OnPlayerExit()
    {
        
    }
}
