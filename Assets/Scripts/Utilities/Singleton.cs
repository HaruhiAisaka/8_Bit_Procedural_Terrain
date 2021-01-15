using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code rewritten from Unity Technologies
/// https://github.com/UnityTechnologies/InputSystem_Warriors/blob/V2/InputSystem_Warriors_Project/Assets/Scripts/Utilities/Singleton.cs
/// Used for classes where there should only be a single instance in the game.
/// Stores the sole instance that is currently being used. Along with other features.
/// </summary>

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();

    // Returns the one instance in the scene if the lock is not active.
    // 
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again = returning null");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] There is more than 1 singleton!");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name =  "(singleton) " + typeof(T).ToString();

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so' " + singleton +
                            "' was created.");
                    }
                }

                return _instance;
            }
        }
    }

    private static bool IsDontDestroyOnLoad()
    {
        if (_instance == null)
        {
            return false;
        }
        // Object exists independend of Scene lifesycle, assume that means it has DontDestroyOnLoad set.
        if ((_instance.gameObject.hideFlags & HideFlags.DontSave) == HideFlags.DontSave)
        {
            return true;
        }
        return false;
    }

    private static bool applicationIsQuitting = false;

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy() 
    {
        if (IsDontDestroyOnLoad())
        {
            applicationIsQuitting = true;
        }
    }
}
