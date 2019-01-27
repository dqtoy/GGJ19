using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactory : Singleton<PrefabFactory>
{
    public PipeSection m_Cross;
    public PipeSection m_Straight1;
    public PipeSection m_Straight2;
    public PipeSection m_Curved1;
    public PipeSection m_Curved2;
    public PipeSection m_Curved3;
    public PipeSection m_Curved4;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
