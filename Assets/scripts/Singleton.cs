/* Joe Snider
 * 3/18
 * 
 * A base class to enforce singleton access.
 * Use it like:
 * class myManager : Singleton<myManager> { ... };
 * 
 * You can include a protected constructor to make sure that it is only created at startup.
 * 
 * The code is copied from http://wiki.unity3d.com/index.php/Toolbox
 * */

using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
/// 

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private bool originalCopy = false;

    public static T instance
    {
        get
        {
            if (!Instantiated) CreateInstance();
            return _instance;
        }
    }

    //TODO: this has gotten pretty spaghetti-like. split it up.
    private static void CreateInstance()
    {
        if (Destroyed)
        {
            //Debug.Log("[Singleton] Attempted to access a destroyed singleton " + type);
            return;
        }

        var type = typeof(T);
        var objects = FindObjectsOfType<T>();

        if (objects.Length == 0)
        {
            //badness
            Debug.Log("[Singletone] Couldn't find a singleton of type " + type +
                ". There must be exactly one in the main scene. Unpredictable (bad) behavior to follow.");
        }
        else if (objects.Length == 1)
        {
            //we're safe to assume that this is the original
            _instance = objects[0];
            Instantiated = true;
            Destroyed = false;
        }
        else
        {
            //more than 1 is floating around. Just keep the one the is 'instantiated'
            Debug.Log("[Singleton] There is more than one instance of Singleton of type \"" + type +
                "\". Keeping the first created one. Destroying the others.");
            bool foundit = false;
            for (var i = 0; i < objects.Length; i++)
            {
                if ((objects[i] as Singleton<T>).originalCopy)
                {
                    if (foundit)
                    {
                        Debug.Log("[Singleton] Found multiple originals of type " + type +
                            ". This is probably bad.");
                    }
                    else
                    {
                        _instance = objects[i];
                        foundit = true;
                    }
                }
                else
                {
                    Destroy(objects[i].gameObject);
                }
            }
            if (!foundit)
            {
                Debug.Log("[Singleton] Couldn't find the original of singleton of type " + type +
                    ". Was it accidentally destroyed elsewhere? This is probably bad.");
            }
            Instantiated = true;
            Destroyed = false;
        }

    }

    [Header("Should this singleton persist across scenes?")]
    public bool Persistent = false;

    public static bool Instantiated { get; private set; }
    public static bool Destroyed { get; private set; }

    protected virtual void Awake()
    {
        var objects = FindObjectsOfType<T>();
        if (objects.Length == 1) { originalCopy = true; }
        CreateInstance();
        if (Persistent) DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        Destroyed = true;
        Instantiated = false;
    }

    public void Touch() { }

}

