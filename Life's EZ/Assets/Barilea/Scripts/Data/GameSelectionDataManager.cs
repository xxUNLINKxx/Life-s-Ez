using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSelectionDataManager : MonoBehaviour
{

    public string[] gameSceneNames;
    public string[] gameNames;
    public string toRemove;

    public int[] choices;
    public int[] gamesLeft;
    public int[] gameNumber;
    public int[] currentGames;
    public int currentNum;
    public int chosenNum;
    public int day;

    public bool[] gameHasBeenPlayed;
    public bool enableControlPanel;
    public GameObject[] controlPanelButtons;
    public GameObject[] gameButtons;
    public GameObject[] textBoxes;
    private SceneTransition sceneTransition;
    void Start()
    {
        BootFile();
        LoadFile();
        ToggleControlPanel();
        sceneTransition = GameObject.Find("sceneTransitionCanvas").GetComponent<SceneTransition>();
        if(gamesLeft == gameNumber)
        {
            GenerateGames();
        }
    }
    public void ResetFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (File.Exists(destination)) File.Delete(destination);
        else return;
        BootFile();
        LoadFile();

    }
    public void onGameSelect(int selected)
    {
        StartCoroutine(gameSelectCoroutine(selected));
    }

    IEnumerator gameSelectCoroutine(int selected)
    {
        sceneTransition.StartCoroutine(sceneTransition.ExitScene(2f));
        yield return new WaitForSeconds(2f);
        gameHasBeenPlayed[(selected-1)] = true;
        SaveFile();
        LoadFile();
        SceneManager.LoadScene(gameSceneNames[currentGames[selected - 1]]);
        sceneTransition.StartCoroutine(sceneTransition.EnterScene());
    }
    
    public void ToggleControlPanel()
    {
        enableControlPanel = !enableControlPanel;
        if(enableControlPanel)
        {
            controlPanelButtons[1].SetActive(true);
            controlPanelButtons[2].SetActive(true);
            controlPanelButtons[3].SetActive(true);
        } else
        {
            controlPanelButtons[1].SetActive(false);
            controlPanelButtons[2].SetActive(false);
            controlPanelButtons[3].SetActive(false);

        }
    }
    public void DeleteFile()

    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (File.Exists(destination)) File.Delete(destination);
        else return;
    }

    public void GenerateGames()
    {
        if (gamesLeft.Length == 0)
        {
            day = 0;
            gamesLeft = gameNumber;
            currentGames = new int[0];
            gameHasBeenPlayed = new bool[0];
            LoadFile();
            return;
        }
        day++;
        if (gamesLeft.Length < 4)
        {
            if (gamesLeft.Length == 1)
            {
                currentGames = new int[1];
                gameHasBeenPlayed = new bool[1];
                currentGames[0] = gamesLeft[0];
                textBoxes[1].GetComponent<Text>().text = gameNames[currentGames[0]];
                textBoxes[2].GetComponent<Text>().text = "No More Games";
                textBoxes[3].GetComponent<Text>().text = "No More Games";
                textBoxes[0].GetComponent<Text>().text = day.ToString();
            }
            else if (gamesLeft.Length == 2)
            {
                currentGames = new int[2];
                gameHasBeenPlayed = new bool[2];
                currentGames[0] = gamesLeft[0];
                currentGames[1] = gamesLeft[1];
                textBoxes[1].GetComponent<Text>().text = gameNames[currentGames[0]];
                textBoxes[2].GetComponent<Text>().text = gameNames[currentGames[1]];
                textBoxes[3].GetComponent<Text>().text = "No More Games";
                textBoxes[0].GetComponent<Text>().text = day.ToString();

            }
            else
            {
                currentGames = new int[3];
                gameHasBeenPlayed = new bool[3];
                currentGames[0] = gamesLeft[0];
                currentGames[1] = gamesLeft[1];
                currentGames[2] = gamesLeft[2];
                textBoxes[1].GetComponent<Text>().text = gameNames[currentGames[0]];
                textBoxes[2].GetComponent<Text>().text = gameNames[currentGames[1]];
                textBoxes[2].GetComponent<Text>().text = gameNames[currentGames[2]];
                textBoxes[0].GetComponent<Text>().text = day.ToString();

            }
            gamesLeft = new int[0];
            SaveFile();
            LoadFile();
            return;
        }
        else
        {
            //Generates Games to be played.
            currentNum = 0;
            choices = new int[3];
            while (currentNum != 3)
            {
                chosenNum = UnityEngine.Random.Range(0, gamesLeft.Length);
                if (choices.Contains(chosenNum))
                {
                }
                else
                {
                    choices[currentNum] = chosenNum;
                    currentNum++;
                }
            }
            //Changes CurrentGames Data to keep track of and record it to display in game.
            currentGames = new int[3];
            gameHasBeenPlayed = new bool[3];
            currentGames[0] = gamesLeft[choices[0]];
            currentGames[1] = gamesLeft[choices[1]];
            currentGames[2] = gamesLeft[choices[2]];
            //Changes gamesLeft to keep track of which games have not been played yet for the next days.
            gamesLeft = gamesLeft.Where(val => val != currentGames[0]).ToArray();
            gamesLeft = gamesLeft.Where(val => val != currentGames[1]).ToArray();
            gamesLeft = gamesLeft.Where(val => val != currentGames[2]).ToArray();
            textBoxes[1].GetComponent<Text>().text = gameNames[currentGames[0]];
            textBoxes[2].GetComponent<Text>().text = gameNames[currentGames[1]];
            textBoxes[2].GetComponent<Text>().text = gameNames[currentGames[2]];
            textBoxes[0].GetComponent<Text>().text = day.ToString();
            SaveFile();
            LoadFile();
        }
    } 
    public void BootFile()
    {
        //Happens During string Boot Stage, Checks if file exist. If Yes, do nothing. If No, Create a new config.
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else
        {
            file = File.Create(destination);
            GameSelectionData data = new GameSelectionData(gameSceneNames, gameNames, gameNumber, gameNumber, new int[0], new bool[3], 0);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
        }
        file.Close();
        ToggleControlPanel();
        ToggleControlPanel();
    }
    public void SaveFile()
    {
        //Saves File.
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }
        GameSelectionData data = new GameSelectionData(gameSceneNames, gameNames, gamesLeft, gameNumber, currentGames, gameHasBeenPlayed, day);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

    }

    public void LoadFile()
    {
        //Loads File.
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameSelectionData data = (GameSelectionData)bf.Deserialize(file);
        gameNames = data.gameNames;
        gameSceneNames = data.gameSceneNames;
        currentGames = data.currentGames;
        gamesLeft = data.gamesLeft;
        gameNumber = data.gameNumber;
        gameHasBeenPlayed = data.gameHasBeenPlayed;
        day = data.day;
        textBoxes[1].GetComponent<Text>().text = (currentGames.Length>0 ? gameNames[currentGames[0]] : "No Game Selected");
        textBoxes[2].GetComponent<Text>().text = (currentGames.Length > 1 ? gameNames[currentGames[1]] : "No Game Selected");
        textBoxes[3].GetComponent<Text>().text = (currentGames.Length > 2 ? gameNames[currentGames[2]] : "No Game Selected");
        textBoxes[0].GetComponent<Text>().text = day.ToString();
        for(int i=1;i<=currentGames.Length;i++)
        {
            if (gameHasBeenPlayed[i - 1] == true)
            {
            Button button = gameButtons[i].GetComponent<Button>();
            ColorBlock color = button.colors;
            color.normalColor = new Color32(174, 255, 244, 255);
            color.highlightedColor = new Color32(37, 223, 255, 255);
            color.selectedColor = new Color32(11, 190, 211, 255);
            color.pressedColor = new Color32(0, 130, 212, 255);
            button.colors = color;

            } else
            {
            Button button = gameButtons[i].GetComponent<Button>();
            ColorBlock color = button.colors;
            color.normalColor = new Color32(183, 255, 174, 255);
            color.highlightedColor = new Color32(100, 241, 0, 255);
            color.selectedColor = new Color32(42, 221, 0, 255);
            color.pressedColor = new Color32(72, 185, 0, 255);
            button.colors = color;

            }
        }
        file.Close();
        gameButtons[0].GetComponent<Button>().interactable = false;
        switch(currentGames.Length)
        {
            default:
                Debug.Log("This should be impossible. This literally cant happen. Contact me immediately if this occurs - Nicholas");
                break;
            case 0:
                gameButtons[0].GetComponent<Button>().interactable = true;
                break;
            case 1:
                if (!gameHasBeenPlayed[0]) return;
                gameButtons[0].GetComponent<Button>().interactable = true;
                break;
            case 2:
                if (!gameHasBeenPlayed[0]) return;
                if (!gameHasBeenPlayed[1]) return;
                gameButtons[0].GetComponent<Button>().interactable = true;
                break;
            case 3:
                if (!gameHasBeenPlayed[0]) return;
                if (!gameHasBeenPlayed[1]) return;
                if (!gameHasBeenPlayed[2]) return;
                gameButtons[0].GetComponent<Button>().interactable = true;
                break;
        }
    }

    public bool canBePressed()
    {
        switch (currentGames.Length)
        {
            default:
                Debug.Log("This should be impossible. This literally cant happen. Contact me immediately if this occurs - Nicholas");
                break;
            case 0:
                break;
            case 1:
                if (!gameHasBeenPlayed[0]) return false;
                break;
            case 2:
                if (!gameHasBeenPlayed[0]) return false;
                if (!gameHasBeenPlayed[1]) return false;
                break;
            case 3:
                if (!gameHasBeenPlayed[0]) return false;
                if (!gameHasBeenPlayed[1]) return false;
                if (!gameHasBeenPlayed[2]) return false;
                break;
        }
        return true;

    }

}
