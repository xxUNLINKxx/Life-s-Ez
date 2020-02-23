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
    [SerializeField] private AudioClip click;
    public AudioSource aSrc;

    private void Start()
    {
        sceneTransition = GameObject.Find("sceneTransitionCanvas").GetComponent<SceneTransition>();
        aSrc = GameObject.Find("aSrc").GetComponent<AudioSource>();
    }
    public void NewGame()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (File.Exists(destination)) File.Delete(destination);
        StartCoroutine(NextDay());
        aSrc.PlayOneShot(click);
    }

    public void Exit()
    {
        aSrc.PlayOneShot(click);
        Application.Quit();
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
