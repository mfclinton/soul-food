using UnityEngine;
using System.Collections.Generic;

public class DataLoader<T> where T : Object
{
    private string path;

    public DataLoader(string resourcePath)
    {
        path = resourcePath;
    }

    internal T[] LoadAll()
    {
        T[] loadedObjects = Resources.LoadAll<T>(path);
        return loadedObjects;
    }
}