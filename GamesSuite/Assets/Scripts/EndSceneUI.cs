using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndSceneUI : MonoBehaviour
{
    private TMP_Text winLoseText;
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
    private TMP_Text winLoseText2;
    void Start() {
        winLoseText = GameObject.Find("Win/Lose Text").GetComponent<TMP_Text>();
        winLoseText2 = GameObject.Find("Win/Lose Text 2").GetComponent<TMP_Text>();
        if (WordlePlayer.playerWin) {
            winLoseText.text = "Congratulations!";
            winLoseText2.text = "You Guessed The Word";
                                
        } else {
            winLoseText.text = "Better Luck Next Time";
            winLoseText2.text = "The correct word was: " + WordlePlayer.correctWord.ToUpper();
=======
>>>>>>> Stashed changes
    void Start() {
        winLoseText = GameObject.Find("Win/Lose Text").GetComponent<TMP_Text>();
        if (WordlePlayer.playerWin) {
            winLoseText.text = "You WON, Congratulations!";
        } else {
            winLoseText.text = "You LOST. \n" +
                                "The correct word was: " + WordlePlayer.correctWord.ToUpper();
<<<<<<< Updated upstream
=======
>>>>>>> be7bb3546e76cdbd8d7c8b3b0e00e42ab5a95fea
>>>>>>> Stashed changes
        }
    }
    
    public void goToMainMenu() {
        SceneManager.LoadScene("StartMenuScene");
    }

    public void goToWordle() {
        SceneManager.LoadScene("Wordle");
    }
}
