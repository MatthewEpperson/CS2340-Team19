using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Credits() {
        Debug.Log("credits");
    }

    public void Settings() {
        Debug.Log("settings");
    }

    public void Games() {
        Debug.Log("games");
    }
}
