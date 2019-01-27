using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PipeSection : MonoBehaviour
{
    public enum Type
    {
        Cross,
        Straight,
        Curved
    };
    
    public enum PipeRotation
    {
        NoRotation,
        Clockwise90,
        Clockwise180,
        Clockwise270
    };

    public Type m_Type = PipeSection.Type.Cross;
    public PipeSection.PipeRotation m_Orientation = PipeRotation.NoRotation;

    public int characterEntry = -1;
    public int characterExit = -1;

    public void OnPlayerEnter(int enterPoint)
    {
        //calculate exit point
        characterEntry = enterPoint;
        characterExit = PipeUtils.GetExitPoint(this, enterPoint);
    }

    public void OnPlayerExit()
    {
        
    }


}
