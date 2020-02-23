using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    private AudioSource aSrc;

    private void Start()
    {
        aSrc = GetComponent<AudioSource>();
    }
    public void tellMeWHYY()
    {
        aSrc.Play();
    }
    public void back2Start()
    {
        SceneManager.LoadScene("Menu");
    }
}
