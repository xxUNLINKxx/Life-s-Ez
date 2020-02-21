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
    SceneTransition sceneTransition;

    private void Start()
    {
        sceneTransition = GameObject.Find("sceneTransitionCanvas").GetComponent<SceneTransition>();
    }
    public void NewGame()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (File.Exists(destination)) File.Delete(destination);
        StartCoroutine(NextDay());
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
    IEnumerator NextDay()
    {
        Debug.Log("This works");
        sceneTransition.StartCoroutine(sceneTransition.ExitScene(2f));
        yield return new WaitForSeconds(2f);
        sceneTransition.StartCoroutine(sceneTransition.EnterScene());
        SceneManager.LoadScene("Main");
    }
}
