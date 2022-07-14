using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseLogic : MonoBehaviour
{
    public void pauseGame() {
        Time.timeScale = 0;
    }

    public void resumeGame() {
        Time.timeScale = 1;
    }

    public void goToStartMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenuScene");
    }

    public void restartWordleGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Wordle");
    }
    public void restartGoGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Go");
    }
}
