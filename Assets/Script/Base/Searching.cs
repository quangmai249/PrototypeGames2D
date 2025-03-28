using UnityEngine;

public static class Searching
{
    public static T ByName<T>(string name, T[] arr) where T : Object
    {
        foreach (T item in arr)
            if (item.name == name)
                return item;
        return null;
    }

    public static GameObject GameObjectByName(string name, string tag)
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(tag))
            if (item.name == name)
                return item;
        return null;
    }
}
