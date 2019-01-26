using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSection : MonoBehaviour
{
    public enum PipeRotation
    {
        NoRotation,
        Clockwise90,
        Clockwise180,
        Clockwise270
    };
    
    public PipeSection.PipeRotation Orientation = PipeRotation.NoRotation; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
