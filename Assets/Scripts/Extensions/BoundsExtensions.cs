using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BoundsExtensions
{
    public static List<Vector3> GetCorners(this Bounds bounds)
    {
        return new List<Vector3>(4)
        {
            bounds.max,
            new Vector3(bounds.min.x, bounds.max.y, 0),
            bounds.min,
            new Vector3(bounds.max.x, bounds.min.y, 0),
        };
    }
}
