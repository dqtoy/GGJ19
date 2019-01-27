using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to keep the orientation of pipes correct in the editor
[ExecuteInEditMode]
public class PipeOrientationSetter : MonoBehaviour
{
    private PipeSection pipeSection;
    
    // Start is called before the first frame update
    void Start()
    {
        pipeSection = gameObject.GetComponent<PipeSection>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (pipeSection.m_Orientation)
        {
            case PipeSection.PipeRotation.NoRotation:
                gameObject.transform.rotation = Quaternion.identity;
                break;
            case PipeSection.PipeRotation.Clockwise90:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case PipeSection.PipeRotation.Clockwise180:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
            case PipeSection.PipeRotation.Clockwise270:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -270);
                break;
        }
    }
}
