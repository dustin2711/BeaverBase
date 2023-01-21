using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    ///   Trys getting component. If null, adds component.
    /// </summary>
    public static Component GetOrAddComponent<Component>(this GameObject gameObject) where Component : UnityEngine.Component
    {
        // This does NOT work (thats why I created this method):
        // component = gameObject.GetComponent<Component>() ?? gameObject.AddComponent<Component>();

        Component component = gameObject.GetComponent<Component>();
        if (component == null)
        {
            component = gameObject.AddComponent<Component>();
        }
        return component;
    }

    /// <summary>
    ///   Resets the local vectors to default values (position 0|0|0, euler 0|0|0, scale 1|1|1).
    /// </summary>
    public static void ResetLocalVectors(this GameObject gameObject, 
        Vector3 localPosition = default, Vector3 localEulerAngles = default, Vector3 localScale = default)
    {
        gameObject.transform.ResetLocalVectors(localPosition, localEulerAngles, localScale);
    }

    /// <summary>
    ///   Destroy all child game objects of the specified game object.
    /// </summary>
    public static void DestroyAllChilds(this GameObject gameObject)
    {
        foreach (Transform child in gameObject.transform)
        {
            Object.Destroy(child.gameObject);
        }
    }
}
