using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    [SerializeField] private string NextLevel;
    [SerializeField] private Animator sceneAnim;
    private bool hasPressed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E)&&!hasPressed)
            {
                hasPressed = true;
                StartCoroutine(NextScene(NextLevel, transitionTime));
            }
            else
            {

            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !hasPressed)
            {
                hasPressed = true;
                StartCoroutine(NextScene(NextLevel, transitionTime));
            }
            else
            {

            }
        }
    }

    IEnumerator NextScene(string level,float time)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(level);
        Time.timeScale = 1;
    }

}
