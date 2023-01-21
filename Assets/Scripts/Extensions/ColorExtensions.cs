using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class ColorExtensions
{
    /// <summary>
    ///   Returns the color with new values where you want to.
    /// </summary>
    public static Color Edited(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
    {
        return new Color(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
    }

    public static Color32 Edited(this Color32 color, byte? r = null, byte? g = null, byte? b = null, byte? a = null)
    {
        return new Color32(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
    }
}
