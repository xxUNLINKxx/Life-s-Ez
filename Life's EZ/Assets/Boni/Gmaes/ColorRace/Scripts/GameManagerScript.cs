using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManagerScript : MonoBehaviour
{
    public float[] timerplevel;
    public GameObject[] levels;
    public GameObject defeattext;
    public GameObject victorytext;
    public int index;
    private GameObject player;
    public TextMeshProUGUI timertext;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            defeattext.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void Victory()
    {
        victorytext.SetActive(true);
    }
    public void ActivateLevel()
    {
        levels[index].SetActive(true);
        levels[index - 1].SetActive(false);
    }
}
