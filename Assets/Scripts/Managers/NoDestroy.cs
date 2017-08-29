using UnityEngine;
using System.Collections;

public class NoDestroy : MonoBehaviour {
    public static NoDestroy Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
