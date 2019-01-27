using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUtils
{
    /// <summary>
    /// check if adjacent blocks connect
    /// </summary>
    /// <returns>The connects.</returns>
    /// <param name="exitPoint">Exit point of current tile.</param>
    /// <param name="neighborType">pipe type id of the neighbor tile.</param>
    static public bool Connects(int enterPoint, int neighborType)
    {
        bool[] accectedEntryPoints = GetConnectPointsByType(neighborType);
        return accectedEntryPoints[enterPoint];
    }

    static public bool Connects(int enterPoint, Tile pipe)
    {
        int typeId = GetTypeId(pipe.m_Type, pipe.m_Orientation);
        return Connects(enterPoint, typeId);
    }

    static public int GetTypeId(Tile.Type mainType, Tile.PipeRotation subType)
    {
        int typeId = 0;
        if (mainType == Tile.Type.CrossPipe)
            typeId = 0;
        else if (mainType == Tile.Type.StraightPipe)
        {
            if (subType == Tile.PipeRotation.NoRotation)
                typeId = 1;
            else typeId = 2;
        }
        else
        {
            if (subType == Tile.PipeRotation.NoRotation)
                typeId = 3;
            else if (subType == Tile.PipeRotation.Clockwise90)
                typeId = 4;
            else if (subType == Tile.PipeRotation.Clockwise180)
                typeId = 5;
            else typeId = 6;
        }

        return typeId;
    }

    static public bool[] GetConnectPointsByType(Tile.Type mainType, Tile.PipeRotation subType)
    {
        int typeId = GetTypeId(mainType, subType);
        return GetConnectPointsByType(typeId);
    }

    static public bool[] GetConnectPointsByType(int typeId)
    {
        bool[] connects;
        switch (typeId)
        {
            case 0:
                connects = new bool[4] { true, true, true, true };
                break;
            case 1:
                connects = new bool[4] { true, false, true, false };
                break;
            case 2:
                connects = new bool[4] { false, true, false, true };
                break;
            case 3:
                connects = new bool[4] { true, false, false, true };
                break;
            case 4:
                connects = new bool[4] { true, true, false, false };
                break;
            case 5:
                connects = new bool[4] { false, true, true, false };
                break;
            case 6:
                connects = new bool[4] { false, false, true, true };
                break;
            default:
                Debug.LogError("Invalid type Id");
                connects = new bool[4] { false, false, false, false };
                break;
        }

        return connects;
    }

    static public int GetExitPoint(Tile pipe, int entryPoint)
    {
        int exitPoint = -1;
        int typeId = GetTypeId(pipe.m_Type, pipe.m_Orientation);
        switch (typeId)
        {
            case 0:
                exitPoint = (entryPoint + 2) % 4;
                break;
            case 1:
                if (entryPoint == 0)
                    exitPoint = 2;
                else if (entryPoint == 2)
                    exitPoint = 0;
                break;
            case 2:
                if (entryPoint == 1)
                    exitPoint = 3;
                else if (entryPoint == 3)
                    exitPoint = 1;
                break;
            case 3:
                if (entryPoint == 0)
                    exitPoint = 3;
                else if (entryPoint == 3)
                    exitPoint = 0;
                break;
            case 4:
                if (entryPoint == 0)
                    exitPoint = 1;
                else if (entryPoint == 1)
                    exitPoint = 0;
                break;
            case 5:
                if (entryPoint == 1)
                    exitPoint = 2;
                else if (entryPoint == 2)
                    exitPoint = 1;
                break;
            case 6:
                if (entryPoint == 2)
                    exitPoint = 3;
                else if (entryPoint == 3)
                    exitPoint = 2;
                break;
        }
        if (exitPoint == -1)
            Debug.LogError("Could not find exit point: " + typeId + " " + entryPoint);
        return exitPoint;
    }


    //-1 is center
    static public Queue<Vector2> GetWayPoints(Tile pipe, int startPoint, int exitPoint)
    {
        GridLocation gridLocation = pipe.GetComponent<GridLocation>();
        Queue<Vector2> queue = new Queue<Vector2>();
        Vector2 wayPoint = gridLocation.GetAnchoredPosition();
        /*
        if (startPoint == 0)
        {
            wayPoint.x -= gridLocation.gridWidth / 2;
        }
        else if (startPoint == 1)
        {
            wayPoint.y += gridLocation.gridHeight / 2;
        }
        else if (startPoint == 2)
        {
            wayPoint.x += gridLocation.gridWidth / 2;
        }
        else if (startPoint == 3)
        {
            wayPoint.y -= gridLocation.gridHeight / 2;
        }
        */
        queue.Enqueue(wayPoint);


        if (exitPoint == 0)
        {
            wayPoint.x -= gridLocation.gridWidth / 2;
        }
        else if (exitPoint == 1)
        {
            wayPoint.y += gridLocation.gridHeight / 2;
        }
        else if (exitPoint == 2)
        {
            wayPoint.x += gridLocation.gridWidth / 2;
        }
        else if (exitPoint == 3)
        {
            wayPoint.y -= gridLocation.gridHeight / 2;
        }
        queue.Enqueue(wayPoint);

        return queue;
    }
}
