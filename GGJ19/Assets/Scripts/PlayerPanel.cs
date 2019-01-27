using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    public List<GameObject> pieceLocations;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform TakePiece()
    {
        if (pieceLocations[0].transform.childCount == 0)
        {
            return null;
        }

        return pieceLocations[0].transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        // If some piece locations are empty, move the later ones to the earlier slots
        for (int i = 0 ; i < pieceLocations.Count ; i++)
        {
            if (pieceLocations[i].transform.childCount == 0)
            {
                for (int j = i + 1; j < pieceLocations.Count; j++)
                {
                    if (pieceLocations[j].transform.childCount > 0)
                    {
                        pieceLocations[j].transform.GetChild(0).SetParent(pieceLocations[i].transform);
                        pieceLocations[i].transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
                        break;
                    }
                }
            }
        }
        
        // Populate the piece at position 5
        if (pieceLocations[pieceLocations.Count - 1].transform.childCount == 0)
        {
            PipeFactory.Instance.SpawnRandomTile(pieceLocations[pieceLocations.Count - 1].transform);
        }
        
    }
}
