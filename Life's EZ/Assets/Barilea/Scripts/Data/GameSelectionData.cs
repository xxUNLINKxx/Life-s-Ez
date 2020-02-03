
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class GameSelectionData
{
    public string[] gameSceneNames;
    public string[] gameNames;
    public int[] gamesLeft;
    public int[] gameNumber;
    public int[] currentGames;
    public int day;
    public bool[] gameHasBeenPlayed;
    public GameSelectionData(string[] gameSceneNames, string[] gameNames,int[] gamesLeft, int[] gameNumber, int[] currentGames, bool[] gameHasBeenPlayed , int day)
    {
        this.gameSceneNames = gameSceneNames;
        this.gameNames = gameNames;
        this.gamesLeft = gamesLeft;
        this.gameNumber = gameNumber;
        this.currentGames = currentGames;
        this.day = day;
        this.gameHasBeenPlayed = gameHasBeenPlayed;
    }
}
