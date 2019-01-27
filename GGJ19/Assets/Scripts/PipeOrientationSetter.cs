using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to keep the orientation of pipes correct in the editor
[ExecuteInEditMode]
public class PipeOrientationSetter : MonoBehaviour
{
    private Tile tile;
    
    // Start is called before the first frame update
    void Start()
    {
        tile = gameObject.GetComponent<Tile>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (tile.m_Orientation)
        {
            case Tile.PipeRotation.NoRotation:
                gameObject.transform.rotation = Quaternion.identity;
                break;
            case Tile.PipeRotation.Clockwise90:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Tile.PipeRotation.Clockwise180:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
            case Tile.PipeRotation.Clockwise270:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -270);
                break;
        }
    }
}
