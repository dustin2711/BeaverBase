using UnityEngine;

public static class MaterialExtensions
{
    /// <summary>
    ///   Sets the emission of the material (smaller 0 means disabled). By default, uses the material's main color.
    /// </summary>
    public static void SetEmission(this Material material, float intensity, Color color = default)
    {
        if (intensity >= 0)
        {
            // Intensity bigger zero? Enable.
            if (color == default)
            {
                // If no color handed over, use material main color
                color = material.color;
            }
            material.SetColor("_EmissionColor", color * intensity);
            material.EnableKeyword("_EMISSION");
        }
        else
        {
            // Intensity smaller zero? Disable.
            material.DisableKeyword("_EMISSION");
        }
    }

    public static void SetSmoothness(this Material material, float smoothness)
    {
        material.SetFloat("_Glossiness", smoothness);
    }

    public static void SetMetallic(this Material material, float metallic)
    {
        material.SetFloat("_Metallic", metallic);
    }

    /// <summary>
    ///   Performs same thing when you change "Rendering Mode" of material in Unity Editor.
    ///   https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html?_ga=2.133186766.1567639842.1579505861-397254692.1542025426
    /// </summary>
    public static void SetRenderMode(this Material standardShaderMaterial, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
        }

    }
}

public enum BlendMode
{
    Opaque,
    Cutout,
    Fade,
    Transparent
}