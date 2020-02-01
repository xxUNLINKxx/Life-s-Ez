using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{

    private bool hasPressed;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            hasPressed = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }        
        
        if (Input.GetKeyDown(KeyCode.Space)&&!hasPressed)
        {
            hasPressed = true;
            StartCoroutine(ExitScene(2f));
        }

    }

    public IEnumerator ExitScene(float delay)
    {
        LeanTween.scaleY(this.gameObject, 0.1f, 1.2f).setEaseOutSine();
        LeanTween.moveLocalY(this.gameObject, 0, 1.2f).setEaseOutSine().setEaseSpring();
        yield return new WaitForSecondsRealtime(0.9f);
        LeanTween.scale(this.gameObject, new Vector2(20,20), 1f).setEaseLinear();
        yield return new WaitForSecondsRealtime(delay);
        StartCoroutine(EnterScene());
    }

    public IEnumerator EnterScene()
    {
        LeanTween.scale(this.gameObject, new Vector2(0.1f, 0.1f), 1f).setEaseLinear();
        yield return new WaitForSecondsRealtime(1f);
        LeanTween.moveLocalY(this.gameObject, -1000, 0.8f).setEaseInSine().setEaseInBack();
        LeanTween.scaleY(this.gameObject, 1f, 1f).setEaseInSine();
        hasPressed = false;
    }
}
