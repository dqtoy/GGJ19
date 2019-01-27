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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
