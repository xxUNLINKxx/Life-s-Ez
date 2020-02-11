using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Color color;
    public GameObject[] buttons;
    public void NewGame()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (File.Exists(destination)) File.Delete(destination);
        SceneManager.LoadScene("Main"); 
    }
    public void LoadGame()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (File.Exists(destination))
        SceneManager.LoadScene("Main");
    }

    private void Update()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (!File.Exists(destination))
        {
            buttons[1].GetComponent<Image>().color = color;
            buttons[1].transform.GetChild(0).GetComponent<Text>().color = color;
        }
    }
}
