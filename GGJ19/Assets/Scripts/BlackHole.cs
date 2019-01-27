using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private float m_RotationsPerSecond = 0.1f;
    private float m_ElapsedTime = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_ElapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_ElapsedTime += Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, (360 * m_RotationsPerSecond * m_ElapsedTime) % 360);
    }
}
