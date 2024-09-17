using UnityEditor;
using UnityEngine;

public abstract class ScriptableSingletonMono<T> : ScriptableObject where T : ScriptableSingletonMono<T>
{
    private static T sInstance;
    public static T Instance {
        get {
            if (sInstance == null)
            {
                sInstance = Resources.Load<T>(typeof(T).Name);
            }
            return sInstance;
        }
    }

    private void Awake()
    {
        sInstance = Resources.Load<T>(typeof(T).Name);
    }
#if UNITY_EDITOR

    protected virtual void OnValidate()
    {
        DeleteSameTypes();
        MoveToResourcesFolder();
    }

    private void MoveToResourcesFolder()
    {
        if (!AssetDatabase.GetAssetPath(this).Contains("Resources"))
        {
            AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(this), "Assets/Resources/" + this.name + ".asset");
        }
    }

    private void DeleteSameTypes()
    {
        if (Resources.FindObjectsOfTypeAll<T>().Length > 1)
        {
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));
        }
    }
#endif

}
