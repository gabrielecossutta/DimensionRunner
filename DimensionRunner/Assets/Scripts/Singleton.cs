using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        //Check if there are other istance of the Script and delete them
        if (Instance != null && Instance != this)
        {
            // Destroy the duplicate
            Destroy(gameObject);
            return;
        }

        // Assign the istance of the Genetic type 
        Instance = this as T;

        // Don't destroy the object when changing scene
        DontDestroyOnLoad(gameObject);
    }
}