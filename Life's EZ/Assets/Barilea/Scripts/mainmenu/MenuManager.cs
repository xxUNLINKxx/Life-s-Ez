using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public GameObject[] buttons;
    public void NewGame()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (File.Exists(destination)) File.Delete(destination);
        SceneManager.LoadScene("Main"); 
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

}
