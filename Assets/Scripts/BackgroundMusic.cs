using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{

    private void Awake()
    {
        int backgroundMusicCount = FindObjectsOfType<BackgroundMusic>().Length;

        // If there's already a BackgroundMusic object, destroy it.
        if (backgroundMusicCount > 1)
        {
            gameObject.SetActive(false); // This is to ENSURE no problems arise from the illegal BackgroundMusic object.
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}