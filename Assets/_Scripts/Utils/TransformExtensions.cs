using UnityEngine;

public static class TransformExtensions
{
    public static Vector3 ReturnAveragePosition(this Transform parent)
    {
        Vector3 averagePosition = Vector3.zero;
        int numberOfChildren = parent.childCount;
        if (numberOfChildren <= 0) return parent.position;
        for (int i = 0; i < numberOfChildren; i++)
        {
            averagePosition += parent.GetChild(i).position;
        }
        averagePosition /= numberOfChildren;
        return averagePosition;
    }

    public static Transform SetX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        return transform;
    }

    public static Transform SetY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        return transform;
    }

    public static Transform SetZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        return transform;
    }

    public static Transform SetLocalX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        return transform;
    }

    public static Transform SetLocalY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        return transform;
    }

    public static Transform SetLocalZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        return transform;
    }

    public static Transform ResetPosition(this Transform transform)
    {
        transform.position = Vector3.zero;
        return transform;
    }

    public static Transform ResetLocalPosition(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        return transform;
    }

    public static Transform ResetRotation(this Transform transform)
    {
        transform.rotation = Quaternion.identity;
        return transform;
    }

    public static Transform ResetLocalRotation(this Transform transform)
    {
        transform.localRotation = Quaternion.identity;
        return transform;
    }

    public static Transform ResetScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
        return transform;
    }

    public static Transform Reset(this Transform transform, bool isLocal = false)
    {
        if (isLocal)
        {
            transform.ResetLocalPosition();
            transform.ResetLocalRotation();
        }
        else
        {
            transform.ResetPosition();
            transform.ResetRotation();
        }
        transform.ResetScale();
        return transform;
    }

    public static Transform DestroyAllChildren(this Transform transform, bool recursive = false)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (recursive)
            {
                transform.GetChild(i).DestroyAllChildren(true);
            }
            Object.Destroy(transform.GetChild(i).gameObject);
        }
        return transform;
    }

    public static Transform DestroyImmediateAllChildren(this Transform transform, bool recursive = false)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (recursive)
            {
                transform.GetChild(i).DestroyImmediateAllChildren(true);
            }
            Object.DestroyImmediate(transform.GetChild(i).gameObject);
        }
        return transform;
    }
}