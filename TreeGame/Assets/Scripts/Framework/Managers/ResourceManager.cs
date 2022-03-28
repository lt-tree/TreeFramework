using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : UnitySingleton<ResourceManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    public T GetAssetCache<T>(string name) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        string path = "Assets/AssetsPackage/" + name;
        UnityEngine.Object target = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
        return target as T;
#endif
    }
}
