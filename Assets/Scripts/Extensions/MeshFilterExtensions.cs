using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class MeshFilterExtensions
{
    /// <summary>
    ///   Uses the mesh and TransformPoint to get the 4 outlining corners. 
    ///   (This method does not work with MeshRenderer.bounds, getting strange values there)
    /// </summary>
    public static List<Vector3> GetGlobalCorners(this MeshFilter filter)
    {
        List<Vector3> corners = filter.mesh.bounds.GetCorners();
        List<Vector3> globalCorners = new List<Vector3>(4);
        foreach (Vector3 corner in corners)
        {
            globalCorners.Add(filter.transform.TransformPoint(corner));
        }
        return globalCorners;
    }
}