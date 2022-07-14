using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private TMP_Text currGame;
    private string[] gamesList = {"Uno", "Wordle", "Go"};
    private int gamesListIndex = 0;
    void Start() {
        currGame = GameObject.Find("ChangingPanel/ChooseGame/SelectedGame").GetComponent<TMP_Text>();
    }

    public void quitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void credits() {
        Debug.Log("credits");
    }

    public void settings() {
        Debug.Log("settings");
    }

    public void games() {
        Debug.Log("games");
    }

    public void nextGame() {
        if (gamesListIndex + 1 >= gamesList.Length) {
            gamesListIndex = 0;
        } else {
            gamesListIndex++;
        }
        currGame.text = gamesList[gamesListIndex];
    }

    public void prevGame() {
        if (gamesListIndex - 1 < 0) {
            gamesListIndex = gamesList.Length - 1;
        } else {
            gamesListIndex--;
        }
        currGame.text = gamesList[gamesListIndex];
    }

    public void playGame() {
        SceneManager.LoadScene(gamesList[gamesListIndex]);
    }
}