using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ASrc : MonoBehaviour
{
    public static ASrc instance = null;
    public string scene1, scene2;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name != scene1 && SceneManager.GetActiveScene().name != scene2)
        {
            Destroy(this.gameObject);
        }   
    }
}
