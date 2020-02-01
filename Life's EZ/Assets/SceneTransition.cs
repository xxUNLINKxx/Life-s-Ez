using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartScene(1f));
        }
    }
    public IEnumerator StartScene(float time)
    {
        LeanTween.moveLocalY(this.gameObject, 0, 1f).setEaseInOutSine();
        yield return new WaitForSecondsRealtime(time);
        LeanTween.scale(this.gameObject, new Vector2(20,20), 0.5f).setEaseInOutSine();
    }
}
