using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions 
{
    private const int TimeoutFramesSlowlyChange = 60;

    /// <summary>
    ///   Rotates the Transform to the direction in Number of frames.
    /// </summary>
    public static IEnumerator SlowlyLookAt(this Transform transform, Vector3 direction, float speed = 0.3f, int timeoutFrames = TimeoutFramesSlowlyChange, float? fixedAngleZ = null)
    {
        yield return SlowlyRotateTo(transform, Quaternion.LookRotation(direction), speed, timeoutFrames, fixedAngleZ);
    }

    /// <summary>
    ///   Rotates the Transform to the targetRotation in Number of frames.
    /// </summary>
    public static IEnumerator SlowlyRotateTo(this Transform transform, Quaternion targetRotation, float speed = 0.3f, int timeoutFrames = TimeoutFramesSlowlyChange, float? fixedAngleZ = null)
    {
        Quaternion? previousRotation = null;
        int i = 0;
        while (true && i++ < timeoutFrames)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed);
            if (transform.rotation == previousRotation)
            {
                break;
            }
            transform.eulerAngles = transform.eulerAngles.Edited(z: fixedAngleZ); // E.g. when Camera must not roll
            previousRotation = transform.rotation;
            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    ///   Moves the Transform to the targetPosition in Number of frames.
    /// </summary>
    public static IEnumerator SlowlyMoveTo(this Transform transform, Vector3 targetPosition, int frames)
    {
        float distance = Vector3.Distance(targetPosition, transform.position);
        float speed = distance / frames;
        Vector3? previousPosition = null;
        int i = 0;
        while (true && i++ < frames)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
            if (transform.position == previousPosition)
            {
                break;
            }
            previousPosition = transform.position;
            yield return new WaitForFixedUpdate();
        }
    }

    public static bool HasDefaultValues(this Transform transform)
    {
        return transform.eulerAngles == Vector3.zero
            && transform.lossyScale == Vector3.one
            && transform.position == Vector3.zero;
    }

    public static bool HasDefaultValuesLocal(this Transform transform)
    {
        return transform.localEulerAngles == Vector3.zero
            && transform.localScale == Vector3.one
            && transform.localPosition == Vector3.zero;
    }

    /// <summary>
    ///   Finds any child or child's child that fits the predicate.
    /// </summary>
    public static Transform FindInChilds(this Transform transform, Predicate<Transform> predicate)
    {
        foreach (Transform child in transform)
        {
            if (predicate.Invoke(child))
            {
                return child;
            }

            Transform childChild = child.FindInChilds(predicate);
            if (childChild != null)
            {
                return childChild;
            }
        }
        return null;
    }

    /// <summary>
    ///   Returns if transform has component.
    /// </summary>
    public static bool HasComponent<Component>(this Transform transform, out Component component)
    {
        component = transform.GetComponent<Component>();
        return component != null && component.ToString() != "null";
    }

    /// <summary>
    ///   Returns if transform has component.
    /// </summary>
    public static bool HasComponent<Component>(this Transform transform)
    {
        return transform.GetComponent<Component>() != null;
    }

    /// <summary>
    ///   Iterates over each final child (also childs' childs) and performs an action.
    /// </summary>
    public static void ForEachFinalChild(this Transform transform, Action<Transform> action)
    {
        if (transform.childCount == 0)
        {
            // When no childs anymore and active => invoke action
            if (transform.gameObject.activeSelf)
            {
                action.Invoke(transform);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.ForEachFinalChild(action);
            }
        }
    }

    /// <summary>
    ///   Get all direct child transforms
    /// </summary>
    public static IEnumerable<Transform> GetChildren(this Transform transform)
    {
        foreach (Transform child in transform.GetComponent<Transform>())
        {
            if (child != transform)
            {
                yield return child;
            }
        }
    }

    /// <summary>
    ///   Get all direct and further child transforms
    /// </summary>
    public static IEnumerable<Transform> GetAllChildren(this Transform transform)
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                yield return child;
            }
        }
    }

    /// <summary>
    ///   Sets the lossy scale by un-parenting, setting scale and parenting.
    /// </summary>
    public static void SetLossyScale(this Transform transform, Vector3 newScale)
    {
        Transform parentSave = transform.parent;

        transform.SetParent(null);
        transform.localScale = newScale;

        transform.parent = parentSave;
    }

    public static void SetPositionX(this Transform transform, float value)
    {
        transform.position = new Vector3(
            value,
            transform.position.y,
            transform.position.z);
    }

    public static void SetPositionY(this Transform transform, float value)
    {
        transform.position = new Vector3(
            transform.position.x,
            value,
            transform.position.z);
    }

    public static void SetPositionZ(this Transform transform, float value)
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            value);
    }

    public static void SetLocalPositionX(this Transform transform, float value)
    {
        transform.localPosition = new Vector3(
            value, 
            transform.localPosition.y, 
            transform.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform transform, float value)
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x, 
            value, 
            transform.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform transform, float value)
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x, 
            transform.localPosition.y, 
            value);
    }

    public static void AddLocalPositionX(this Transform transform, float value)
    {
        transform.localPosition += new Vector3(
            value,
            0,
            0);
    }

    public static void AddLocalPositionY(this Transform transform, float value)
    {
        transform.localPosition += new Vector3(
            0, 
            value,
            0);
    }

    public static void AddLocalPositionZ(this Transform transform, float value)
    {
        transform.localPosition += new Vector3(
            0,  
            0,
            value);
    }

    public static void SetLocalScaleX(this Transform transform, float value)
    {
        transform.localScale = new Vector3(
            value,
            transform.localScale.y,
            transform.localScale.z);
    }

    public static void SetLocalScaleY(this Transform transform, float value)
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            value,
            transform.localScale.z);
    }

    public static void SetLocalScaleZ(this Transform transform, float value)
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y,
            value);
    }

    public static void SetLocalEulerX(this Transform transform, float value)
    {
        transform.localEulerAngles = new Vector3(
            value,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z);
    }

    public static void SetLocalEulerY(this Transform transform, float value)
    {
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            value,
            transform.localEulerAngles.z);
    }

    public static void SetLocalEulerZ(this Transform transform, float value)
    {
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            value);
    }

    /// <summary>
    ///   Transforms position from local space to world space with parent scale simulated to 1|1|1.
    /// </summary>
    public static Vector3 TransformPointLossy(this Transform transform, Vector3 vector)
    {
        Vector3 previousLossyScale = transform.lossyScale;
        transform.SetLossyScale(Vector3.one);
        Vector3 position = transform.TransformPoint(vector);
        transform.SetLossyScale(previousLossyScale);
        return position;
    }

    public static void SetParent(this Transform transform, Transform parent, bool setLocalPositionZero = false)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    ///   Resets the local vectors to default values (position 0|0|0, euler 0|0|0, scale 1|1|1).
    /// </summary>
    public static void ResetLocalVectors(this Transform transform, 
        Vector3 localPosition = default, Vector3 localEulerAngles = default, Vector3 localScale = default)
    {
        transform.localPosition = localPosition;
        transform.localEulerAngles = localEulerAngles;
        transform.localScale = localScale == default ? Vector3.one : localScale;
    }

}
