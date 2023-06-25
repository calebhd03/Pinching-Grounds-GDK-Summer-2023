using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTheme : MonoBehaviour
{
    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }
}
