using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndSceneUI : MonoBehaviour
{
    private TMP_Text winLoseText;
    void Start() {
        winLoseText = GameObject.Find("Win/Lose Text").GetComponent<TMP_Text>();
        if (WordlePlayer.playerWin) {
            winLoseText.text = "You WON, Congratulations!";
        } else {
            winLoseText.text = "You LOST. \n" +
                                "The correct word was: " + WordlePlayer.correctWord.ToUpper();
        }
    }
    
    public void goToMainMenu() {
        SceneManager.LoadScene("StartMenuScene");
    }

    public void goToWordle() {
        SceneManager.LoadScene("Wordle");
    }
}
