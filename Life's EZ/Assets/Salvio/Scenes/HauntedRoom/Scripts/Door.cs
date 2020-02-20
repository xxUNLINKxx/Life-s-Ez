using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    [SerializeField] private string NextLevel;
    [SerializeField] private Animator anim;
    private bool hasPressed;
    [SerializeField] private GameObject pressE;

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
                pressE.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressE.SetActive(false);
    }

    IEnumerator NextScene(string level,float time)
    {
        anim.SetBool("play", false);
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(level);
    }

}
