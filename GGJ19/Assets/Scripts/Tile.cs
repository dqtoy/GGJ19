using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Tile : MonoBehaviour
{
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

    public int characterEntry = -1;
    public int characterExit = -1;

    public void OnPlayerEnter(int enterPoint)
    {
        //calculate exit point
        characterEntry = enterPoint;
        characterExit = TileUtils.GetExitPoint(this, enterPoint);
        KidCharacterController.Instance.UpdateWayPoint();
    }

    public void OnPlayerExit()
    {
        
    }
}
