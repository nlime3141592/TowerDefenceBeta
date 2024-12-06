using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Path
{
    public int indexFrom;
    public List<int> indexTo;
    public Color pathColor = Color.white;

    public int GetNextPoint()
    {
        System.Random prng = new System.Random();

        if (indexTo == null || indexTo.Count == 0)
            return indexFrom;

        return indexTo[prng.Next(indexTo.Count)];
    }
}