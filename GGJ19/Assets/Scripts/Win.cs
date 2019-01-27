using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public FadeInOut item;

    private void Start()
    {
        item.Play();
    }
}
