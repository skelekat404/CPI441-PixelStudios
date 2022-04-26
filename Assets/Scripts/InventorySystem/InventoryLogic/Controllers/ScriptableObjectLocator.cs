
#if UNITY_EDITOR
    using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "SOLocator", menuName = "ScriptableObjects/ScriptableObjectLocator")]
public class ScriptableObjectLocator : ScriptableObject
{
    [SerializeField] private UDictionary<uint, ScriptableObject> m_ScriptableObjects = new UDictionary<uint, ScriptableObject>();

    public UDictionary<uint, ScriptableObject> ScriptableObjects => m_ScriptableObjects;
    #if UNITY_EDITOR
    public void AddAllScriptableObjectsContainingUIDs<T>() where T : ScriptableObject
    {
        uint key = (uint)m_ScriptableObjects.Dictionary.Count;
        
        foreach (ScriptableObject obj in GetAllInstances<T>())
        {
            if (!m_ScriptableObjects.ContainsValue(obj))
            {
                while (m_ScriptableObjects.ContainsKey(key)) // While uid is not set
                {
                    key++;
                    // Try to set value
                    TryGetProperty("Uid", obj).SetValue(obj, key);
                }
                m_ScriptableObjects.Add(key, obj);
            }
        }
    }
    #endif

    public void ClearAllScriptableObjects()
    {
        m_ScriptableObjects.Clear();
    }
    #if UNITY_EDITOR
    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }
        
        return a;
        
    }
    #endif

    private System.Reflection.PropertyInfo TryGetProperty(string propString, object obj)
    {
        System.Reflection.PropertyInfo prop = obj.GetType().GetProperty("Uid");
        return prop != null ? prop: null;
    }
}