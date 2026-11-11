using UnityEngine;

public static class SceneUtils
{
    public static GameObject FindIncludingInactive(string objectName)
    {
        var all = Object.FindObjectsByType<Transform>(
            FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var t in all)
        {
            if (t.name == objectName && t.gameObject.scene.IsValid())
                return t.gameObject;
        }
        return null;
    }
}