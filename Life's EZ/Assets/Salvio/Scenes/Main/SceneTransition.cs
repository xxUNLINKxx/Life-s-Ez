﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{   
    private static SceneTransition instance; 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            return;
        }
        if(instance==this)
        {
            Destroy(instance);
        }
    }
    
    public GameObject scenetransition;
    private bool hasPressed;
    public IEnumerator ExitScene(float delay)
    {
        LeanTween.scaleY(scenetransition, 0.1f, 1.2f).setEaseOutSine();
        LeanTween.moveLocalY(scenetransition, 0, 1.2f).setEaseOutSine().setEaseSpring();
        yield return new WaitForSecondsRealtime(0.9f);
        LeanTween.scale(scenetransition, new Vector2(20,20), 1f).setEaseLinear();
        yield return new WaitForSecondsRealtime(delay);
    }

    public IEnumerator EnterScene()
    {
        LeanTween.scale(scenetransition, new Vector2(0.1f, 0.1f), 1f).setEaseLinear();
        yield return new WaitForSecondsRealtime(1f);
        LeanTween.moveLocalY(scenetransition, -1000, 0.8f).setEaseInSine().setEaseInBack();
        LeanTween.scaleY(scenetransition, 1f, 1f).setEaseInSine();
        hasPressed = false;
    }
}
