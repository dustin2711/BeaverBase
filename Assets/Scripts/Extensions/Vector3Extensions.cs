using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions
{
    public static bool IsAbout(this Vector2 vector, Vector2 other, float epsilon = 0.0001f)
    {
        return vector.x.IsAbout(other.x, epsilon)
            && vector.y.IsAbout(other.y, epsilon);
    }

    public static string ToSizeString(this Vector2 size, int decimalPlaces = 3)
    {
        return $"{size.x.ToString(decimalPlaces)} x {size.y.ToString(decimalPlaces)}";
    }
}

class Vector2Comparer : IEqualityComparer<Vector2>
{
    public bool Equals(Vector2 a, Vector2 b)
    {
        //Console.WriteLine("Equals: point {0} - point {1}", a, b);
        return a.IsAbout(b);
    }

    public int GetHashCode(Vector2 point)
    {
        //Console.WriteLine("HashCode: {0}", point);
        return point.x.GetHashCode()
            ^ point.y.GetHashCode();
    }
}

public static class Vector3Extensions
{
    /// <summary>
    ///   Returns the vector moves to the target by distance.
    /// </summary>
    public static Vector3 MovedInDirection(this Vector3 vector, Vector3 target, float distance)
    {
        return vector + (target - vector).normalized * distance;
    }

    /// <summary>
    ///   Returns the vector moved along the groundplane. Uses Matchbox orientation.
    /// </summary>
    public static Vector3 MovedInDirection(this Vector3 vector, float orientationRad, float distance)
    {
        var old = vector;
        vector.x -= Mathf.Sin(orientationRad) * distance;
        vector.z += Mathf.Cos(orientationRad) * distance;
        return vector;
    }

    /// <summary>
    ///   Returns a clone of Vector3 with the possibility of setting new values.
    /// </summary>
    public static Vector3 Cloned(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(
            x ?? vector.x,
            y ?? vector.y,
            z ?? vector.z);
    }

    /// <summary>
    ///   Returns an edited vector.
    /// </summary>
    public static Vector3 Edited(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        // Settingt only copy of vector
        vector.Set(
            x ?? vector.x,
            y ?? vector.y,
            z ?? vector.z);
        return vector;
    }

    /// <summary>
    ///   Returns an vector with the values added.
    /// </summary>
    public static Vector3 Added(this Vector3 vector, float x = 0, float y = 0, float z = 0)
    {
        vector.Set(
            vector.x + x,
            vector.y + y,
            vector.z + z);
        return vector;
    }

    /// <summary>
    ///   Returns an vector with the values each multiplied.
    /// </summary>
    public static Vector3 Multiplied(this Vector3 vector, float x = 0, float y = 0, float z = 0)
    {
        vector.Set(
            vector.x * x,
            vector.y * y,
            vector.z * z);
        return vector;
    }

    /// <summary>
    ///   Returns a clamped vector.
    /// </summary>  
    public static Vector3 Clamped(this Vector3 vector,
        float? xMin = null, float? xMax = null,
        float? yMin = null, float? yMax = null,
        float? zMin = null, float? zMax = null) 
    {
        return new Vector3(
            Mathf.Clamp(vector.x, xMin ?? vector.x, xMax ?? vector.x),
            Mathf.Clamp(vector.y, yMin ?? vector.y, yMax ?? vector.y),
            Mathf.Clamp(vector.z, zMin ?? vector.z, zMax ?? vector.z));
    }

    /// <summary>
    ///   Returns a clamped vector but reduces input values by 360° if they are above 180° so range is -180° to +180°.
    /// </summary>  
    public static Vector3 ClampedEuler(this Vector3 vector,
        float? xMin = null, float? xMax = null,
        float? yMin = null, float? yMax = null,
        float? zMin = null, float? zMax = null)
    {
        return new Vector3(
            Mathf.Clamp((vector.x >= 180) ? vector.x - 360 : vector.x, xMin ?? vector.x, xMax ?? vector.x),
            Mathf.Clamp((vector.y >= 180) ? vector.y - 360 : vector.y, yMin ?? vector.y, yMax ?? vector.y),
            Mathf.Clamp((vector.z >= 180) ? vector.z - 360 : vector.z, zMin ?? vector.z, zMax ?? vector.z));
    }

    /// <summary>
    ///   Gets the Matchbox orientation of the vector in radian. (assumes flat world)
    /// </summary>
    public static float MBOrientationRad(this Vector3 vector)
    {
        return -Mathf.Atan2(vector.x, vector.z);
    }

    /// <summary>
    ///   Gets the Matchbox orientation of the vector in degrees - 90
    /// </summary>
    public static float MBOrientation(this Vector3 vector)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(vector.x, vector.z) - 90;
    }
}
